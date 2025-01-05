using OkalaCryptoQuote.Domain.Base;

namespace OkalaCryptoQuote.Domain.Tests.Base;

public class ErrorTests
{
    [Theory]
    [InlineData("test1", "a", ErrorType.None)]
    [InlineData("test2", "", ErrorType.None)]
    [InlineData("test3", null, ErrorType.None)]
    [InlineData("test4", "a", ErrorType.Forbidden)]
    [InlineData("test5", "", ErrorType.Unauthorized)]
    [InlineData("test6", null, ErrorType.Invalid)]
    [InlineData(null, null, ErrorType.Invalid)]
    public void Constructor_Call_CodeEqualsInputValue(string code, string description, ErrorType errorType)
    {
        var error = new Error(code, description, errorType);

        Assert.Equal(code, error.Code);
    }

    [Theory]
    [InlineData("a", "test1", ErrorType.None)]
    [InlineData("", "test2", ErrorType.None)]
    [InlineData(null, "test3", ErrorType.None)]
    [InlineData("a", "test1", ErrorType.Forbidden)]
    [InlineData("", "test2", ErrorType.Unauthorized)]
    [InlineData(null, "test3", ErrorType.Invalid)]
    public void Constructor_Call_DescriptionEqualsInputValue(string code, string description, ErrorType errorType)
    {
        var error = new Error(code, description, errorType);

        Assert.Equal(description, error.Description);
    }

    [Theory]
    [InlineData("a", "test1", ErrorType.None)]
    [InlineData("", "test2", ErrorType.Forbidden)]
    [InlineData(null, "test3", ErrorType.Unauthorized)]
    [InlineData("a", "test1", ErrorType.Invalid)]
    [InlineData("", "test2", ErrorType.Conflict)]
    [InlineData(null, "test3", ErrorType.Problem)]
    [InlineData("test1", "a", ErrorType.NotFound)]
    [InlineData("test2", "", ErrorType.Forbidden)]
    [InlineData("test3", null, ErrorType.Unauthorized)]
    [InlineData("test1", "a", ErrorType.Invalid)]
    [InlineData("test2", "", ErrorType.None)]
    [InlineData("test3", null, ErrorType.Conflict)]
    public void Constructor_Call_ErrorTypeEqualsInputValue(string code, string description, ErrorType errorType)
    {
        var error = new Error(code, description, errorType);

        Assert.Equal(description, error.Description);
    }

    [Fact]
    public void None_Call_Call_CodeEqualsEmptyString()
    {
        var error = Error.None;

        Assert.Equal(string.Empty, error.Code);
    }

    [Fact]
    public void None_Call_DescriptionEqualsEmptyString()
    {
        var error = Error.None;

        Assert.Equal(string.Empty, error.Description);
    }

    [Fact]
    public void None_Call_ErrorTypeEqualsNone()
    {
        var error = Error.None;

        Assert.Equal(ErrorType.None, error.Type);
    }

    [Fact]
    public void NullValue_Call_CodeEqualsExpected()
    {
        var error = Error.NullValue;

        Assert.Equal("General.Null", error.Code);
    }

    [Fact]
    public void NullValue_Call_DescriptionEqualsExpected()
    {
        var error = Error.NullValue;

        Assert.Equal("Null value was provided", error.Description);
    }

    [Fact]
    public void NullValue_Call_ErrorTypeEqualsNone()
    {
        var error = Error.NullValue;

        Assert.Equal(ErrorType.None, error.Type);
    }

    [Fact]
    public void NotFound_Call_CodeEqualsExpected()
    {
        var error = Error.NotFound("feature", "desc");

        Assert.Equal("feature." + nameof(NotFound_Call_CodeEqualsExpected), error.Code);
    }

    [Fact]
    public void NotFound_Call_DescriptionEqualsExpected()
    {
        var error = Error.NotFound("", "test");

        Assert.Equal("test", error.Description);
    }

    [Fact]
    public void NotFound_Call_ErrorTypeEqualsNotFound()
    {
        var error = Error.NotFound("", "");

        Assert.Equal(ErrorType.NotFound, error.Type);
    }

    [Fact]
    public void Problem_Call_CodeEqualsExpected()
    {
        var error = Error.Problem("feature", "desc");

        Assert.Equal("feature." + nameof(Problem_Call_CodeEqualsExpected), error.Code);
    }

    [Fact]
    public void Problem_Call_DescriptionEqualsExpected()
    {
        var error = Error.Problem("", "test");

        Assert.Equal("test", error.Description);
    }

    [Fact]
    public void Problem_Call_ErrorTypeEqualsProblem()
    {
        var error = Error.Problem("", "");

        Assert.Equal(ErrorType.Problem, error.Type);
    }

    [Fact]
    public void Conflict_Call_CodeEqualsExpected()
    {
        var error = Error.Conflict("feature", "desc");

        Assert.Equal("feature." + nameof(Conflict_Call_CodeEqualsExpected), error.Code);
    }

    [Fact]
    public void Conflict_Call_DescriptionEqualsExpected()
    {
        var error = Error.Conflict("", "test");

        Assert.Equal("test", error.Description);
    }

    [Fact]
    public void Conflict_Call_ErrorTypeEqualsConflict()
    {
        var error = Error.Conflict("", "");

        Assert.Equal(ErrorType.Conflict, error.Type);
    }

    [Fact]
    public void Forbidden_Call_CodeEqualsExpected()
    {
        var error = Error.Forbidden("feature", "desc");

        Assert.Equal("feature." + nameof(Forbidden_Call_CodeEqualsExpected), error.Code);
    }

    [Fact]
    public void Forbidden_Call_DescriptionEqualsExpected()
    {
        var error = Error.Forbidden("", "test");

        Assert.Equal("test", error.Description);
    }

    [Fact]
    public void Forbidden_Call_ErrorTypeEqualsForbidden()
    {
        var error = Error.Forbidden("", "");

        Assert.Equal(ErrorType.Forbidden, error.Type);
    }

    [Fact]
    public void Unauthorized_Call_CodeEqualsExpected()
    {
        var error = Error.Unauthorized("feature", "desc");

        Assert.Equal("feature." + nameof(Unauthorized_Call_CodeEqualsExpected), error.Code);
    }

    [Fact]
    public void Unauthorized_Call_DescriptionEqualsExpected()
    {
        var error = Error.Unauthorized("", "test");

        Assert.Equal("test", error.Description);
    }

    [Fact]
    public void Unauthorized_Call_ErrorTypeEqualsUnauthorized()
    {
        var error = Error.Unauthorized("", "");

        Assert.Equal(ErrorType.Unauthorized, error.Type);
    }

    [Fact]
    public void Invalid_Call_CodeEqualsExpected()
    {
        var error = Error.Invalid("feature", "desc");

        Assert.Equal("feature." + nameof(Invalid_Call_CodeEqualsExpected), error.Code);
    }

    [Fact]
    public void Invalid_Call_DescriptionEqualsExpected()
    {
        var error = Error.Invalid("", "test");

        Assert.Equal("test", error.Description);
    }

    [Fact]
    public void Invalid_Call_ErrorTypeEqualsInvalid()
    {
        var error = Error.Invalid("", "");

        Assert.Equal(ErrorType.Invalid, error.Type);
    }

    [Fact]
    public void ImplicitOperator_ErrorToResult_ErrorEqualsExpected()
    {
        var testValue = Error.Problem("feature", "desc");

        Result result = testValue;

        Assert.NotNull(result.Error);
        Assert.Equal("desc", result.Error.Description);
    }
}
