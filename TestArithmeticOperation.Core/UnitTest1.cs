using ArithmeticOperation.Core;

namespace TestArithmeticOperation.Core
{
    public class UnitTest1
    {
        [Fact]
        public void TestExtractExpression()
        {
            var calculator = new ArithmeticOperations();
            string expression = "3+5*2-4/2";
            calculator.ExtractExpression(expression);
            Assert.Equal(new List<string> { "+", "*", "-", "/" }, calculator.operators);
            Assert.Equal(new List<double> { 3, 5, 2, 4, 2 }, calculator.numbers);
        }

        [Fact]
        public void TestIsAbleToCalculate()
        {
            var calculator = new ArithmeticOperations();
            Assert.True(calculator.IsAbleToCalculate("3+5*2-4/2"));
            Assert.False(calculator.IsAbleToCalculate("3+5*2-4/"));
            Assert.False(calculator.IsAbleToCalculate("25-"));
            Assert.True(calculator.IsAbleToCalculate("3+5*2-4/2+1"));
        }

        [Fact]
        public void TestCalculate()
        {
            var calculator = new ArithmeticOperations();
            Assert.Equal("11", calculator.Calculate("3+5*2-4/2"));
            Assert.Equal("6", calculator.Calculate("10-3*2+4/2"));
            Assert.Equal("23", calculator.Calculate("2*3+4*5-6/2"));
        }

        [Fact]
        public void TestCalculateWithNegativeNumbers()
        {
            var calculator = new ArithmeticOperations();
            Assert.Equal("5", calculator.Calculate("-3+5*2-4/2"));
            Assert.Equal("-2", calculator.Calculate("-10+3*2+4/2"));
            Assert.Equal("11", calculator.Calculate("-2*3+4*5-6/2"));
        }

        [Fact]
        public void TestCalculateWithDecimalNumbers()
        {
            var calculator = new ArithmeticOperations();
            Assert.Equal("11.5", calculator.Calculate("3.5+5*2-4/2"));
            Assert.Equal("6", calculator.Calculate("10-3*2+4/2.0"));
            Assert.Equal("23", calculator.Calculate("2*3+4*5-6/2.0"));
        }
    }
}