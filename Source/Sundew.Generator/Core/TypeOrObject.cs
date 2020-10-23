// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeOrObject.cs" company="Hukano">
// Copyright (c) Hukano. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Sundew.Generator.Core
{
    using System;
    using Sundew.Generator.Internal;
    using Sundew.Generator.Output;

    /// <summary>
    /// Represents a type or an object.
    /// </summary>
    /// <typeparam name="TObject">The type of the object.</typeparam>
    public class TypeOrObject<TObject> : IHaveType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TypeOrObject{TObject}"/> class.
        /// </summary>
        public TypeOrObject()
        {
            this.Type = typeof(TObject);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeOrObject{TObject}"/> class.
        /// </summary>
        /// <param name="object">The object.</param>
        public TypeOrObject(TObject @object)
        {
            this.Object = @object;
            this.Type = @object.GetType();
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public Type Type { get; }

        /// <summary>
        /// Gets the object.
        /// </summary>
        /// <value>
        /// The object.
        /// </value>
        public TObject Object { get; }

        /// <summary>
        /// Performs an implicit conversion from <see cref="IWriter" /> to <see cref="TypeOrObject{TObject}" />.
        /// </summary>
        /// <param name="outputter">The writer.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator TypeOrObject<TObject>(TObject outputter)
        {
            return new TypeOrObject<TObject>(outputter);
        }
    }
}