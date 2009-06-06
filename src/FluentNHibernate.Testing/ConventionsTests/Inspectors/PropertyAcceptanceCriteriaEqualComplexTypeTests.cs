using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.DslImplementation;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.MappingModel;
using FluentNHibernate.Testing.DomainModel;
using NUnit.Framework;
using Is=FluentNHibernate.Conventions.AcceptanceCriteria.Is;

namespace FluentNHibernate.Testing.ConventionsTests.Inspectors
{
    [TestFixture]
    public class PropertyAcceptanceCriteriaEqualComplexTypeTests
    {
        private IAcceptanceCriteria<IPropertyInspector> acceptance;

        [SetUp]
        public void CreateAcceptanceCriteria()
        {
            acceptance = new ConcreteAcceptanceCriteria<IPropertyInspector>();
        }

        [Test]
        public void ExpectEqualShouldValidateToTrueIfGivenMatchingModel()
        {
            acceptance.Expect(x => x.Access, Is.Equal(Access.AsField()));

            acceptance
                .Matches(new PropertyDsl(new PropertyMapping(typeof(Record)) { Access = "field" }))
                .ShouldBeTrue();
        }

        [Test]
        public void ExpectEqualShouldValidateToFalseIfNotGivenMatchingModel()
        {
            acceptance.Expect(x => x.Access, Is.Equal(Access.AsField()));

            acceptance
                .Matches(new PropertyDsl(new PropertyMapping(typeof(Record)) { Access = "property" }))
                .ShouldBeFalse();
        }

        [Test]
        public void ExpectEqualShouldValidateToFalseIfUnset()
        {
            acceptance.Expect(x => x.Access, Is.Equal(Access.AsField()));

            acceptance
                .Matches(new PropertyDsl(new PropertyMapping(typeof(Record))))
                .ShouldBeFalse();
        }

        [Test]
        public void ExpectNotEqualShouldValidateToTrueIfGivenMatchingModel()
        {
            acceptance.Expect(x => x.Access, Is.Not.Equal(Access.AsField()));

            acceptance
                .Matches(new PropertyDsl(new PropertyMapping(typeof(Record)) { Access = "property" }))
                .ShouldBeTrue();
        }

        [Test]
        public void ExpectNotEqualShouldValidateToFalseIfNotGivenMatchingModel()
        {
            acceptance.Expect(x => x.Access, Is.Not.Equal(Access.AsField()));

            acceptance
                .Matches(new PropertyDsl(new PropertyMapping(typeof(Record)) { Access = "field" }))
                .ShouldBeFalse();
        }

        [Test]
        public void ExpectNotEqualShouldValidateToTrueIfUnset()
        {
            acceptance.Expect(x => x.Access, Is.Not.Equal(Access.AsField()));

            acceptance
                .Matches(new PropertyDsl(new PropertyMapping(typeof(Record))))
                .ShouldBeTrue();
        }
    }
}