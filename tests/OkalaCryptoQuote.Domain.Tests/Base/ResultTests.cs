using OkalaCryptoQuote.Domain.Base;

namespace OkalaCryptoQuote.Domain.Tests.Base;

public class ResultTests
{
    [Fact]
    public void Success_WithoutError_BeSuccess()
    {
        var result = Result.Success();

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public void Success_WithoutError_ErrorIsNull()
    {
        var result = Result.Success();

        Assert.Null(result.Error);
    }

    [Fact]
    public void Failure_WithError_BeFailed()
    {
        var result = Result.Failure(Error.Problem("test", "test"));

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public void Failure_WithError_HasError()
    {
        var result = Result.Failure(Error.Problem("test", "test"));

        Assert.NotNull(result.Error);
    }

    [Fact]
    public void GenericSuccess_WithoutError_BeSuccess()
    {
        var result = Result.Success(1);

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public void GenericSuccess_WithoutError_ErrorIsNull()
    {
        var result = Result.Success(1);

        Assert.Null(result.Error);
    }

    [Fact]
    public void GenericSuccess_WithoutError_HasValueAsExpected()
    {
        var result = Result.Success(1);

        Assert.Equal(1, result.Value);
    }

    [Fact]
    public void GenericFailure_WithError_BeFailed()
    {
        var result = Result.Failure<int>(Error.Problem("test", "test"));

        Assert.False(result.IsSuccess);
    }

    [Fact]
    public void GenericFailure_WithError_HasError()
    {
        var result = Result.Failure<int>(Error.Problem("test", "test"));

        Assert.NotNull(result.Error);
    }

    public class TestClass();

    [Fact]
    public void GenericFailure_WithErrorAndReferenceTypeGeneric_ValueIsNull()
    {
        var result = Result.Failure<TestClass>(Error.Problem("test", "test"));

        Assert.Null(result.Value);
    }

    [Fact]
    public void GenericFailure_WithErrorAndPrimitiveTypeGeneric_ValueIsNull()
    {
        var result = Result.Failure<int>(Error.Problem("test", "test"));

        Assert.Equal(default, result.Value);
    }

    [Fact]
    public void ImplicitOperator_ValueToResult_ValueEqualsExpected()
    {
        const string testValue = "Test";

        Result<string> result = testValue;

        Assert.Equal(testValue, result.Value);
    }

    [Fact]
    public void ImplicitOperator_ErrorToResult_ErrorEqualsExpected()
    {
        var testValue = Error.Problem("feature", "desc");

        Result<string> result = testValue;

        Assert.NotNull(result.Error);
        Assert.Equal("desc", result.Error.Description);
    }

}
