﻿using System;
using System.Collections;
using System.Collections.Generic;
using FluentNHibernate.Automapping.Steps;
using FluentNHibernate.Conventions;
using FluentNHibernate.Mapping;

namespace FluentNHibernate.Automapping
{
    /// <summary>
    /// Implement this interface to control how the automapper behaves.
    /// </summary>
    public interface IAutomappingConfiguration
    {
        /// <summary>
        /// Determines whether a type should be auto-mapped.
        /// Override to restrict which types are mapped in your domain.
        /// </summary>
        /// <remarks>
        /// You normally want to override this method and restrict via something known, like
        /// Namespace.
        /// </remarks>
        /// <example>
        /// return type.Namespace.EndsWith("Domain");
        /// </example>
        /// <param name="type">Type to map</param>
        /// <returns>Should map type</returns>
        bool ShouldMap(Type type);

        /// <summary>
        /// Determines whether a member of a type should be auto-mapped.
        /// Override to restrict which members are considered in automapping.
        /// </summary>
        /// <remarks>
        /// You normally want to override this method to restrict which members will be
        /// used for mapping. This method will be called for every property, field, and method
        /// on your types.
        /// </remarks>
        /// <example>
        /// // all writable public properties:
        /// return member.IsProperty && member.IsPublic && member.CanWrite;
        /// </example>
        /// <param name="member">Member to map</param>
        /// <returns>Should map member</returns>
        bool ShouldMap(Member member);

        /// <summary>
        /// Determines whether a member is the id of an entity.
        /// </summary>
        /// <remarks>
        /// This method is called for each member that ShouldMap(Type) returns true for.
        /// </remarks>
        /// <param name="member">Member</param>
        /// <returns>Member is id</returns>
        bool IsId(Member member);

        /// <summary>
        /// Gets the access strategy to be used for a read-only property. This method is
        /// called for every setterless property and private-setter autoproperty in your
        /// domain that has been accepted through <see cref="ShouldMap(FluentNHibernate.Member)"/>.
        /// </summary>
        /// <param name="member">Member to get access strategy for</param>
        /// <returns>Access strategy</returns>
        Access GetAccessStrategyForReadOnlyProperty(Member member);

        Type GetParentSideForManyToMany(Type left, Type right);
        bool IsConcreteBaseType(Type type);
        bool IsComponent(Type type);
        string GetComponentColumnPrefix(Member member);
        bool IsDiscriminated(Type type);
        string GetDiscriminatorColumn(Type type);
        SubclassStrategy GetSubclassStrategy(Type type);

        /// <summary>
        /// Specifies whether an abstract type is considered a Layer Supertype
        /// (http://martinfowler.com/eaaCatalog/layerSupertype.html). Defaults to
        /// true for all abstract classes. Override this method if you have an
        /// abstract class that you want mapping as a regular entity.
        /// </summary>
        /// <param name="type">Abstract class type</param>
        /// <returns>Whether the type is a Layer Supertype</returns>
        bool AbstractClassIsLayerSupertype(Type type);

        string SimpleTypeCollectionValueColumn(Member member);

        /// <summary>
        /// Gets the steps that are executed to map a type.
        /// </summary>
        /// <returns>Collection of mapping steps</returns>
        // TODO: Remove need for ConventionFinder and AutoMapper references here
        IEnumerable<IAutomappingStep> GetMappingSteps(AutoMapper mapper, IConventionFinder conventionFinder);
    }
}