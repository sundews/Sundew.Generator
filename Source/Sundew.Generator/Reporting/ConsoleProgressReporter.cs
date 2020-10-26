// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleProgressReporter.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Reporting
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;
    using Sundew.Base.Text;

    /// <summary>
    /// Prints generator progress info to the <see cref="Console"/>.
    /// </summary>
    public class ConsoleProgressReporter : IProgressReporter
    {
        private const string DoneText = "Done";
        private const string ProcessingText = "Processing...";
        private const string InitializingText = "Initializing...";
        private const char SpaceChar = ' ';
        private static readonly string[] BusyIndicator = { "-", "\\", "|", "/" };

        private readonly Stopwatch stopwatch = new Stopwatch();
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private readonly BlockingCollection<Base.Computation.Progress<Report>> reports = new BlockingCollection<Base.Computation.Progress<Report>>(new ConcurrentQueue<Base.Computation.Progress<Report>>());
        private readonly bool isRedirected;
        private Task outputTask;
        private int busyIndicatorIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleProgressReporter"/> class.
        /// </summary>
        public ConsoleProgressReporter()
        {
            this.isRedirected = Console.IsOutputRedirected;
        }

        /// <summary>
        /// Reports the specified progress.
        /// </summary>
        /// <param name="progress">The progress.</param>
        public void Report(Sundew.Base.Computation.Progress<Report> progress)
        {
            this.reports.Add(progress);
            if (progress.Report != null && progress.Report.ReportType == ReportType.CompletedGeneration)
            {
                this.reports.CompleteAdding();
            }
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            this.stopwatch.Start();
            this.outputTask = Task.Run(this.Output, this.cancellationTokenSource.Token);
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            this.cancellationTokenSource.Cancel();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.outputTask?.Wait();
            this.cancellationTokenSource.Dispose();
        }

        private static void Print(IEnumerable<ConsoleLine> consoleLines)
        {
            foreach (var consoleLine in consoleLines)
            {
                if (consoleLine.OverwriteLastLine)
                {
                    var lineNumber = Console.CursorTop;
                    Console.CursorLeft = 0;
                    Console.Write(new string(SpaceChar, Console.WindowWidth));
                    Console.SetCursorPosition(0, lineNumber);
                }

                Console.Write(consoleLine.Text);
            }
        }

        private Task Output()
        {
            Base.Computation.Progress<Report> previousProgress = null;
            while (!this.cancellationTokenSource.Token.IsCancellationRequested && !this.reports.IsCompleted)
            {
                if (this.reports.TryTake(out var progress, TimeSpan.FromMilliseconds(100)))
                {
                    var consoleLines = this.GetLines(progress, previousProgress);

                    Print(consoleLines);

                    previousProgress = progress;
                }
                else if (previousProgress != null && !previousProgress.HasCompletedAdding && !this.isRedirected)
                {
                    Print(this.GetBusyIndicatorLine(previousProgress));
                }
            }

            return Task.CompletedTask;
        }

        private IEnumerable<ConsoleLine> GetLines(Base.Computation.Progress<Report> progress, Base.Computation.Progress<Report> previousProgress)
        {
            if (progress.Report != null)
            {
                switch (progress.Report.ReportType)
                {
                    case ReportType.StartingGeneration:
                        if (!this.stopwatch.IsRunning)
                        {
                            this.stopwatch.Start();
                        }

                        yield return new ConsoleLine(false, $"Starting generation...{Environment.NewLine}");
                        break;
                    case ReportType.AddingItems:
                        if (this.isRedirected)
                        {
                            if (previousProgress != null && previousProgress.TotalItems == 0)
                            {
                                yield return new ConsoleLine(false, $"Initializing...{Environment.NewLine}");
                            }
                        }
                        else
                        {
                            yield return
                                new ConsoleLine(true, this.GetProgressMessage(progress, this.GetProcessValueAndTick()));
                        }

                        break;
                    case ReportType.GeneratedItem:
                    case ReportType.AppliedContent:
                    case ReportType.CompletedTarget:
                        if (this.isRedirected)
                        {
                            var text = progress.Report.Parameter.ToString();
                            yield return new ConsoleLine(false, $"{text.LimitAndPad(Math.Max(60, text.Length), ' ', PadSide.Right, LimitSide.Left)} - {progress.Percentage,8:P}{Environment.NewLine}");
                        }
                        else
                        {
                            yield return new ConsoleLine(true, $"{progress.Report.Parameter.ToString().LimitAndPad(Console.BufferWidth - 11, ' ', PadSide.Right, LimitSide.Left)} - {progress.Percentage,8:P}{Environment.NewLine}");
                            yield return new ConsoleLine(false, this.GetProgressMessage(progress, this.GetProcessValueAndTick()));
                        }

                        break;
                    case ReportType.TargetChanged:
                        break;
                    case ReportType.CompletedGeneration:
                        this.busyIndicatorIndex = 0;
                        this.stopwatch.Stop();
                        yield return new ConsoleLine(false, $"{(this.isRedirected ? string.Empty : Environment.NewLine)}Completed generation in {this.stopwatch.Elapsed}{Environment.NewLine}");
                        break;
                    case ReportType.Error:
                        yield return new ConsoleLine(false, Environment.NewLine);
                        this.cancellationTokenSource.Cancel();
                        break;
                    case ReportType.Cancelled:
                        yield return new ConsoleLine(false, $"{Environment.NewLine}Generation cancelled{Environment.NewLine}");
                        this.cancellationTokenSource.Cancel();
                        break;
                }
            }
        }

        private string GetProcessValueAndTick()
        {
            return BusyIndicator[this.busyIndicatorIndex++ % 4];
        }

        private IEnumerable<ConsoleLine> GetBusyIndicatorLine(Base.Computation.Progress<Report> previousProgress)
        {
            if (!this.isRedirected)
            {
                yield return
                    new ConsoleLine(true, this.GetProgressMessage(previousProgress, this.GetProcessValueAndTick()));
            }
        }

        private string GetProgressMessage(Base.Computation.Progress<Report> progress, string processUnknownValue)
        {
            var ending = this.isRedirected ? Environment.NewLine : string.Empty;
            var progressValue = progress.HasCompletedAdding ? $"{progress.Percentage,8:P}" : processUnknownValue;
            var actualProgress = progress.IsCompleted ? string.Empty : $" - {progressValue}";
            var action = progress.HasCompletedAdding ? progress.IsCompleted ? DoneText : ProcessingText : InitializingText;
            return $"{action,-19} completed {progress.CompletedItems + " / " + progress.TotalItems + " items",-30}" + actualProgress + ending;
        }
    }
}