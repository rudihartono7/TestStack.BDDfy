using System;
using System.Collections.Generic;
using System.Reflection;
using Bddify.Core;
using Bddify.Scanners;
using NUnit.Framework;
using System.Linq;

namespace Bddify.Tests.FluentStepScanner
{
    public enum SomeEnumForTesting
    {
        Value1,
        Value2
    }

    [Story
        (AsA = "As a programmer",
        IWant = "I want to be able to use fluent api to scan for steps",
        SoThat = "So that I can be in full control of what is passed in")]
    public class BddifyUsingFluentApi
    {
        private string[] _arrayInput1;
        private int[] _arrayInput2;
        private int _primitiveInput2;
        private string _primitiveInput1;
        private SomeEnumForTesting _enumInput;
        private Action _action;

        public void GivenAnAction(Action actionInput)
        {
            _action = actionInput;    
        }

        public void ThenCallingTheActionThrows<T>() where T : Exception
        {
            Assert.Throws<T>(() => _action());
        }

        public void GivenPrimitiveInputs(string input1, int input2)
        {
            _primitiveInput1 = input1;
            _primitiveInput2 = input2;
        }

        public void GivenEnumInputs(SomeEnumForTesting input)
        {
            _enumInput = input;
        }

        public void GivenArrayInputs(string[] input1, int[] input2)
        {
            _arrayInput1 = input1;
            _arrayInput2 = input2;
        }

        public void GivenEnumerableInputs(IEnumerable<string> input1, IEnumerable<int> input2)
        {
            _arrayInput1 = input1.ToArray();
            _arrayInput2 = input2.ToArray();
        }

        public void ThenTheArgumentsArePassedInProperlyAndStoredOnTheSameObjectInstance(string expectedInput1, int expectedInput2)
        {
            Assert.That(_primitiveInput1, Is.EqualTo(expectedInput1));
            Assert.That(_primitiveInput2, Is.EqualTo(expectedInput2));
        }

        public void ThenEnumArgumentIsPassedInProperlyAndStoredOnTheSameObjectInstance(SomeEnumForTesting expectedInput)
        {
            Assert.That(_enumInput, Is.EqualTo(expectedInput));
        }

        public void ThenTheArgumentsArePassedInProperlyAndStoredOnTheSameObjectInstance(IEnumerable<string> expectedInput1, IEnumerable<int> expectedInput2)
        {
            Assert.That(_arrayInput1, Is.EqualTo(expectedInput1));
            Assert.That(_arrayInput2, Is.EqualTo(expectedInput2));
        }

        string _primitiveInput1Field = "1";
        int _primitiveInput2Field = 2;

        SomeEnumForTesting _enumInputField = SomeEnumForTesting.Value2;

        public string PrimitiveInput1Property { get { return _primitiveInput1Field; } }
        public int PrimitiveInput2Property { get { return _primitiveInput2Field; } }

        public SomeEnumForTesting EnumInputProperty { get { return _enumInputField; } }

        string[] _arrayInput1Field = new[] { "1", "2" };
        int[] _arrayInput2Field = new[] { 3, 4 };

        private IEnumerable<string> EnumerableString = new[] {"1", null, "2"};
        private IEnumerable<int> EnumerableInt = new[] {1, 2};

        public string[] ArrayInput1Property { get { return _arrayInput1Field; } }
        public int[] ArrayInput2Property { get { return _arrayInput2Field; } }

        [Test]
        public void PassingPrimitiveArgumentsInline()
        {
            FluentStepScanner<BddifyUsingFluentApi>
                .Scan()
                .Given(x => x.GivenPrimitiveInputs("1", 2), "Given inline input arguments {0} and {1}")
                .Then(x => x.ThenTheArgumentsArePassedInProperlyAndStoredOnTheSameObjectInstance("1", 2))
                .Bddify();
        }

        [Test]
        public void PassingNullPrimitiveArgumentInline()
        {
            FluentStepScanner<BddifyUsingFluentApi>
                .Scan()
                .Given(x => x.GivenPrimitiveInputs(null, 2), "Given inline input arguments {0} and {1}")
                .Then(x => x.ThenTheArgumentsArePassedInProperlyAndStoredOnTheSameObjectInstance(null, 2))
                .Bddify();
        }

        [Test]
        public void PassingPrimitiveArgumentsUsingVariables()
        {
            var input1 = "1";
            var input2 = 2;

            FluentStepScanner<BddifyUsingFluentApi>
                .Scan()
                .Given(x => x.GivenPrimitiveInputs(input1, input2), "Given input arguments {0} and {1} are passed in using varialbles")
                .Then(x => x.ThenTheArgumentsArePassedInProperlyAndStoredOnTheSameObjectInstance(input1, input2))
                .Bddify();
        }

        [Test]
        public void PassingNullAsPrimitiveArgumentsUsingVariables()
        {
            string input1 = null;
            var input2 = 2;

            FluentStepScanner<BddifyUsingFluentApi>
                .Scan()
                .Given(x => x.GivenPrimitiveInputs(input1, input2), "Given input arguments {0} and {1} are passed in using varialbles")
                .Then(x => x.ThenTheArgumentsArePassedInProperlyAndStoredOnTheSameObjectInstance(input1, input2))
                .Bddify();
        }
 
        [Test]
        public void PassingPrimitiveArgumentsUsingFields()
        {
            FluentStepScanner<BddifyUsingFluentApi>
                .Scan()
                .Given(x => x.GivenPrimitiveInputs(_primitiveInput1Field, _primitiveInput2Field), "Given input arguments {0} and {1} are passed in using fields")
                .Then(x => x.ThenTheArgumentsArePassedInProperlyAndStoredOnTheSameObjectInstance("1", 2))
                .Bddify();
        }
 
        [Test]
        public void PassingPrimitiveArgumentsUsingProperties()
        {
            FluentStepScanner<BddifyUsingFluentApi>
                .Scan()
                .Given(x => x.GivenPrimitiveInputs(PrimitiveInput1Property, PrimitiveInput2Property), "Given input arguments {0} and {1} are passed in using properties")
                .Then(x => x.ThenTheArgumentsArePassedInProperlyAndStoredOnTheSameObjectInstance("1", 2))
                .Bddify();
        }

        [Test]
        public void PassingEnumArgumentInline()
        {
            FluentStepScanner<BddifyUsingFluentApi>
                .Scan()
                .Given(x => x.GivenEnumInputs(SomeEnumForTesting.Value1), "Given inline enum argument {0}")
                .Then(x => x.ThenEnumArgumentIsPassedInProperlyAndStoredOnTheSameObjectInstance(SomeEnumForTesting.Value1))
                .Bddify();
        }

        [Test]
        public void PassingEnumArgumentUsingVariable()
        {
            var someEnumForTesting = SomeEnumForTesting.Value1;
            FluentStepScanner<BddifyUsingFluentApi>
                .Scan()
                .Given(x => x.GivenEnumInputs(someEnumForTesting), "Given enum argument {0} provided using variable")
                .Then(x => x.ThenEnumArgumentIsPassedInProperlyAndStoredOnTheSameObjectInstance(someEnumForTesting))
                .Bddify();
        }

        [Test]
        public void PassingEnumArgumentUsingFields()
        {
            FluentStepScanner<BddifyUsingFluentApi>
                .Scan()
                .Given(x => x.GivenEnumInputs(_enumInputField), "Given enum argument {0} provided using fields")
                .Then(x => x.ThenEnumArgumentIsPassedInProperlyAndStoredOnTheSameObjectInstance(_enumInputField))
                .Bddify();
        }

        [Test]
        public void PassingArrayArgumentsInline()
        {
            FluentStepScanner<BddifyUsingFluentApi>
                .Scan()
                .Given(x => x.GivenArrayInputs(new[] { "1", "2" }, new[] { 3, 4 }), "Given inline array input arguments")
                .Then(x => x.ThenTheArgumentsArePassedInProperlyAndStoredOnTheSameObjectInstance(new[] { "1", "2" }, new[] { 3, 4 }))
                .Bddify();
        }

        [Test]
        public void PassingEnumerableArguments()
        {
            FluentStepScanner<BddifyUsingFluentApi>
                .Scan()
                .Given(x => x.GivenEnumerableInputs(EnumerableString, EnumerableInt), "Given enumerable input arguments")
                .Then(x => x.ThenTheArgumentsArePassedInProperlyAndStoredOnTheSameObjectInstance(EnumerableString, EnumerableInt))
                .Bddify();
        }
        
        [Test]
        public void PassingNullArrayArgumentInline()
        {
            FluentStepScanner<BddifyUsingFluentApi>
                .Scan()
                .Given(x => x.GivenArrayInputs(new[] {"1", null, "2"}, new[] {1, 2}), "Given inline input arguments {0} and {1}")
                    .Then(x => x.ThenTheArgumentsArePassedInProperlyAndStoredOnTheSameObjectInstance(new[] { "1", null, "2" }, new[] { 1, 2 }))
                .Bddify();
        }

        [Test]
        public void PassingNullAsArrayArgumentInline()
        {
            FluentStepScanner<BddifyUsingFluentApi>
                .Scan()
                .Given(x => x.GivenArrayInputs(null, new[] {1, 2}), "Given inline input arguments {0} and {1}")
                    .Then(x => x.ThenTheArgumentsArePassedInProperlyAndStoredOnTheSameObjectInstance(null, new[] { 1, 2 }))
                .Bddify();
        }

        [Test]
        public void PassingArrayArgumentsUsingVariables()
        {
            var input1 = new[] {"1", "2"};
            var input2 = new[] {3, 4};

            FluentStepScanner<BddifyUsingFluentApi>
                .Scan()
                .Given(x => x.GivenArrayInputs(input1, input2), "Given array input arguments {0} and {1} are passed in using variables")
                .Then(x => x.ThenTheArgumentsArePassedInProperlyAndStoredOnTheSameObjectInstance(input1, input2))
                .Bddify();
        }

        [Test]
        public void PassingNullAsOneOfArrayArgumentUsingVariables()
        {
            var input1 = new[] {null, "2"};
            var input2 = new[] {3, 4};

            FluentStepScanner<BddifyUsingFluentApi>
                .Scan()
                .Given(x => x.GivenArrayInputs(input1, input2), "Given array input arguments {0} and {1} are passed in using variables")
                .Then(x => x.ThenTheArgumentsArePassedInProperlyAndStoredOnTheSameObjectInstance(input1, input2))
                .Bddify();
        }
 
        [Test]
        public void PassingArrayArgumentsUsingFields()
        {
            FluentStepScanner<BddifyUsingFluentApi>
                .Scan()
                .Given(x => x.GivenArrayInputs(_arrayInput1Field, _arrayInput2Field), "Given array input arguments {0} and {1} are passed in using fields")
                .Then(x => x.ThenTheArgumentsArePassedInProperlyAndStoredOnTheSameObjectInstance(new[] { "1", "2" }, new[] { 3, 4 }))
                .Bddify();
        }
 
        [Test]
        public void PassingArrayArgumentsUsingProperties()
        {
            FluentStepScanner<BddifyUsingFluentApi>
                .Scan()
                .Given(x => x.GivenArrayInputs(ArrayInput1Property, ArrayInput2Property), "Given array input arguments {0} and {1} are passed in using properties")
                .Then(x => x.ThenTheArgumentsArePassedInProperlyAndStoredOnTheSameObjectInstance(new[] { "1", "2" }, new[] { 3, 4 }))
                .Bddify();
        }

        [Test]
        public void WhenTitleIsNotProvidedItIsFetchedFromMethodName()
        {
            var story = 
                FluentStepScanner<BddifyUsingFluentApi>
                    .Scan()
                    .Given(x => x.GivenPrimitiveInputs("1", 2))
                    .Then(x => x.ThenTheArgumentsArePassedInProperlyAndStoredOnTheSameObjectInstance("1", 2))
                    .Bddify();

            var scenario = story.Scenarios.First();
            Assert.That(scenario.ScenarioText, Is.EqualTo(NetToString.Convert(MethodBase.GetCurrentMethod().Name)));
        }

        [Test]
        public void WhenTitleIsProvidedItIsUsedAsIs()
        {
            const string dummyTitle = "some dummy title; blah blah $#^";
            var story = 
                FluentStepScanner<BddifyUsingFluentApi>
                    .Scan()
                    .Given(x => x.GivenPrimitiveInputs("1", 2))
                    .Then(x => x.ThenTheArgumentsArePassedInProperlyAndStoredOnTheSameObjectInstance("1", 2))
                    .Bddify(dummyTitle);

            var scenario = story.Scenarios.First();
            Assert.That(scenario.ScenarioText, Is.EqualTo(dummyTitle));
        }

        private void ExceptionThrowingAction()
        {
            throw new ApplicationException();
        }

        [Test]
        public void CanPassActionToFluentApi()
        {
            FluentStepScanner<BddifyUsingFluentApi>
                .Scan()
                .Given(x => x.GivenAnAction(ExceptionThrowingAction))
                .Then(x => x.ThenCallingTheActionThrows<ApplicationException>())
                .Bddify();
        }

        [Test]
        public void CanPassActionAndTitleToFluentApi()
        {
            FluentStepScanner<BddifyUsingFluentApi>
                .Scan()
                .Given(x => x.GivenAnAction(ExceptionThrowingAction), "Given an action that throws AppliationException")
                .Then(x => x.ThenCallingTheActionThrows<ApplicationException>(), "Then calling the action does throw that exception")
                .Bddify();
        }
    }
}