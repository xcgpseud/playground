using DataStructures;
using DataStructures.Exceptions;
using DataStructures.Models;
using FluentAssertions;
using NUnit.Framework;
using UnitTests.Models;

namespace UnitTests.LinkedListTests;

[TestFixture]
public class DeleteTests : LinkedListTestBase
{
    [Test]
    public void Delete_CorrectlyRemovesNodeAtGivenPosition()
    {
        // Arrange
        var testData = GenerateTestData(3);
        var (intValues, intList) = testData.GetIntData();
        var (stringValues, stringList) = testData.GetStringData();
        var (boolValues, boolList) = testData.GetBoolData();
        var (testModelValues, testModelList) = testData.GetTestModelData();

        var intNode = intList.GetHeadNode().NextNode;
        var stringNode = stringList.GetHeadNode().NextNode;
        var boolNode = boolList.GetHeadNode().NextNode;
        var testModelNode = testModelList.GetHeadNode().NextNode;

        intNode.Should().NotBeNull();
        stringNode.Should().NotBeNull();
        boolNode.Should().NotBeNull();
        testModelNode.Should().NotBeNull();

        // Act
        intList.Delete(intNode);
        stringList.Delete(stringNode);
        boolList.Delete(boolNode);
        testModelList.Delete(testModelNode);

        // Assert
        AssertLinkedListOrder(intList, [intValues[0], intValues[2]]);
        AssertLinkedListOrder(stringList, [stringValues[0], stringValues[2]]);
        AssertLinkedListOrder(boolList, [boolValues[0], boolValues[2]]);
        AssertLinkedListOrder(testModelList, [testModelValues[0], testModelValues[2]]);
    }

    [Test]
    public void Delete_HeadNode_WithExistingNextNode_SetsHeadAsNextNode()
    {
        // Arrange
        var testData = GenerateTestData(3);
        var (intValues, intList) = testData.GetIntData();
        var (stringValues, stringList) = testData.GetStringData();
        var (boolValues, boolList) = testData.GetBoolData();
        var (testModelValues, testModelList) = testData.GetTestModelData();

        // Act
        intList.Delete(intList.GetHeadNode());
        stringList.Delete(stringList.GetHeadNode());
        boolList.Delete(boolList.GetHeadNode());
        testModelList.Delete(testModelList.GetHeadNode());

        // Assert
        AssertLinkedListOrder(intList, [intValues[1], intValues[2]]);
        AssertLinkedListOrder(stringList, [stringValues[1], stringValues[2]]);
        AssertLinkedListOrder(boolList, [boolValues[1], boolValues[2]]);
        AssertLinkedListOrder(testModelList, [testModelValues[1], testModelValues[2]]);
    }

    [Test]
    public void Delete_HeadNode_WithNoExistingNextNode_SetsHeadAsEmptyNode_WithDefaultValue()
    {
        // Arrange
        var testData = GenerateTestData(1);
        var intList = testData.IntLinkedList;
        var stringList = testData.StringLinkedList;
        var boolList = testData.BoolLinkedList;
        var testModelList = testData.TestModelLinkedList;

        // Act
        intList.Delete(intList.GetHeadNode());
        stringList.Delete(stringList.GetHeadNode());
        boolList.Delete(boolList.GetHeadNode());
        testModelList.Delete(testModelList.GetHeadNode());

        // Assert
        intList.GetHeadNode().Should().BeOfType<Node<int>>();
        intList.GetHeadNode().Value.Should().Be(default);

        stringList.GetHeadNode().Should().BeOfType<Node<string>>();
        stringList.GetHeadNode().Value.Should().Be(default);

        boolList.GetHeadNode().Should().BeOfType<Node<bool>>();
        boolList.GetHeadNode().Value.Should().Be(default);

        testModelList.GetHeadNode().Should().BeOfType<Node<TestModel>>();
        testModelList.GetHeadNode().Value.Should().Be(default);
    }

    [Test]
    public void Delete_LastNode_CorrectlyRemovesLastNode()
    {
        // Arrange
        var testData = GenerateTestData(2);
        var (intValues, intList) = testData.GetIntData();
        var (stringValues, stringList) = testData.GetStringData();
        var (boolValues, boolList) = testData.GetBoolData();
        var (testModelValues, testModelList) = testData.GetTestModelData();

        var intNode = intList.GetHeadNode().NextNode;
        var stringNode = stringList.GetHeadNode().NextNode;
        var boolNode = boolList.GetHeadNode().NextNode;
        var testModelNode = testModelList.GetHeadNode().NextNode;

        intNode.Should().NotBeNull();
        stringNode.Should().NotBeNull();
        boolNode.Should().NotBeNull();
        testModelNode.Should().NotBeNull();

        // Act
        intList.Delete(intNode);
        stringList.Delete(stringNode);
        boolList.Delete(boolNode);
        testModelList.Delete(testModelNode);

        // Assert
        AssertLinkedListOrder(intList, [intValues[0]]);
        AssertLinkedListOrder(stringList, [stringValues[0]]);
        AssertLinkedListOrder(boolList, [boolValues[0]]);
        AssertLinkedListOrder(testModelList, [testModelValues[0]]);
    }

    [Test]
    public void Delete_OutOfBoundsPosition_ThrowsException()
    {
        // Arrange
        var testData = GenerateTestData(1);
        var intList = testData.IntLinkedList;
        var stringList = testData.StringLinkedList;
        var boolList = testData.BoolLinkedList;
        var testModelList = testData.TestModelLinkedList;

        // Act
        var intAction = () => intList.Delete(new Node<int> { Value = TestIntValue });
        var stringAction = () => stringList.Delete(new Node<string> { Value = TestStringValue });
        var boolAction = () => boolList.Delete(new Node<bool> { Value = TestBoolValue });
        var testModelAction = () => testModelList.Delete(new Node<TestModel> { Value = TestModelValue });

        // Assert
        intAction.Should().Throw<NodeNotFoundInListException>();
        stringAction.Should().Throw<NodeNotFoundInListException>();
        boolAction.Should().Throw<NodeNotFoundInListException>();
        testModelAction.Should().Throw<NodeNotFoundInListException>();
    }
}