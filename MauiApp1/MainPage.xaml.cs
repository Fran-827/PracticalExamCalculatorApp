using System;
using Microsoft.Maui.Controls;
using ArithmeticOperation.Core;

namespace MauiApp1;

public partial class MainPage : ContentPage
{
    private string currentExpression = string.Empty;
    private bool justCalculated = false;
    private readonly ArithmeticOperations calculator = new ArithmeticOperations();

    public MainPage()
    {
        InitializeComponent();
        UpdateDisplay();
    }

    private void OnClearClicked(object sender, EventArgs e)
    {
        currentExpression = string.Empty;
        UpdateDisplay();
        DisplayLabel2.Text = string.Empty;
    }

    private void OnButtonClicked(object sender, EventArgs e)
    {
        if (sender is not Button btn)
            return;

        var key = btn.Text ?? string.Empty;
        var operators = new[] { "+", "-", "*", "/" };

        if (justCalculated)
        {
            if (!operators.Contains(key))
            {
                currentExpression = string.Empty;
            }
            justCalculated = false;
        }

        if (int.TryParse(key, out _))
        {
            if (currentExpression == "0")
            {
                currentExpression = key;
            }
            else
            {
                currentExpression += key;
            }
            UpdatePartialResult();
            UpdateDisplay();
            return;
        }

        if (key == ".")
        {
            if (string.IsNullOrEmpty(currentExpression) || operators.Contains(currentExpression[^1].ToString()))
            {
                currentExpression += "0.";
                UpdateDisplay();
                return;
            }

            var lastOpIndex = currentExpression.LastIndexOfAny(new[] { '+', '-', '*', '/' });
            var currentNumber = lastOpIndex >= 0 ? currentExpression[(lastOpIndex + 1)..] : currentExpression;

            if (!currentNumber.Contains("."))
            {
                currentExpression += ".";
                UpdateDisplay();
            }

            return;
        }

        if (operators.Contains(key))
        {
            if (string.IsNullOrEmpty(currentExpression))
            {
                if (key == "-")
                {
                    currentExpression = "-";
                    UpdateDisplay();
                }

                return;
            }

            var lastChar = currentExpression[^1].ToString();
            if (operators.Contains(lastChar))
            {
                if (key == "-" && lastChar != "-")
                {
                    currentExpression += "-";
                    UpdateDisplay();
                    return;
                }

                currentExpression = currentExpression[..^1] + key;
                UpdateDisplay();
                return;
            }

            currentExpression += key;
            UpdateDisplay();
            return;
        }
    }

    private void OnEqualsClicked(object sender, EventArgs e)
    {
        currentExpression = calculator.IsAbleToCalculate(currentExpression) ? calculator.Calculate(currentExpression) : currentExpression;
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        DisplayLabel1.Text = string.IsNullOrEmpty(currentExpression) ? "0" : currentExpression;
    }

    private void UpdatePartialResult()
    {
        if (calculator.IsAbleToCalculate(currentExpression))
        {
            var result = calculator.Calculate(currentExpression);
            DisplayLabel2.Text = result;
        }
        else
        {
            DisplayLabel2.Text = string.Empty;
        }
    }

}
