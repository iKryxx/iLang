using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;

namespace Own_Language;
public class iLangVisitor:iLangBaseVisitor<object?>
{
    private Dictionary<string, object?> Variables { get; } = new();

    public iLangVisitor()
    {
        Variables["Write"] = new Func<object?[], object?>(Write);
    }

    private object? Write(object?[] args)
    {
        foreach(var arg in args)
        {
            Console.Write(arg);
        }
        Console.WriteLine();
        return null;
    }


    public override object VisitFunctionCall([NotNull] iLangParser.FunctionCallContext context)
    {
        var name = context.IDENTIFIER().GetText();
        var args =  context.expression().Select(Visit).ToArray();

        if (!Variables.ContainsKey(name)) throw new Exception($"Function '{name}' is not defined");

        if (!(Variables[name] is Func<object?[], object?> func)) throw new Exception($"'{name}' is not a function");
        return func(args);
    }

    public override object? VisitAssignment(iLangParser.AssignmentContext context)
    {
        // i = 3
        // j = false
        var varName = context.IDENTIFIER().GetText();   //Get the name of an Assignment (i or j)

        var value = Visit(context.expression());        //Get the value of an Assignment (3 or false)

        Variables[varName] = value;                     //Store it in the Variables Dict

        return null;
    }
    public override object? VisitConstant([NotNull] iLangParser.ConstantContext context)
    {
        //Console.WriteLine(context.GetText());
        if (context.INTEGER() is { } i) return int.Parse(i.GetText());
        if (context.FLOAT() is { } f) return float.Parse(f.GetText());
        if (context.STRING() is { } s) return s.GetText().Substring(1,s.GetText().Length - 2);
        if (context.BOOL() is { } b) return bool.Parse(b.GetText());
        if (context.NULL() is { } n) return null;

        throw new NotImplementedException("Valuetype unknown.");
    }

    public override object VisitParenthesizedExpression([NotNull] iLangParser.ParenthesizedExpressionContext context)
    {
        return Visit(context.expression());
    }

    public override object? VisitIdentifierExpression([NotNull] iLangParser.IdentifierExpressionContext context)
    {
        var varNAme = context.IDENTIFIER().GetText();

        if (!Variables.ContainsKey(varNAme))
            throw new Exception($"variable {varNAme} is not defined.");

        return Variables[varNAme];
    }
    public override object? VisitAdditiveExpression([NotNull] iLangParser.AdditiveExpressionContext context)
    {
        var left = Visit(context.expression(0));
        var right = Visit(context.expression(1));

        var op = context.addOp().GetText();

        return op switch
        {
            "+" => Add(left, right),
            "-" => Subtract(left, right),
            _ => throw new NotImplementedException($"operator '{op}' is not defined.")
        };
    }
    private object? Add(object? left, object? right)
    {
        
        if(left is int l && right is int r)
            return l + r;

        if(left is float fl && right is float fr)
            return fl + fr;

        if(left is int lInt && right is float rFloat)
            return lInt + rFloat;

        if(left is float flFloat && right is int frInt)
            return flFloat + frInt;

        if(left is string || right is string)
            return $"{left}{right}"; 

        throw new Exception($"Cannot apply '+' to values of types {left?.GetType()} and {right?.GetType()}");
    }
    private object? Subtract(object? left, object? right)
    {

        if (left is int l && right is int r)
            return l - r;

        if (left is float fl && right is float fr)
            return fl - fr;

        if (left is int lInt && right is float rFloat)
            return lInt - rFloat;

        if (left is float flFloat && right is int frInt)
            return flFloat - frInt;

        if (left is string s && (right is int || right is float || right is string))
            return s.Replace(right.ToString(), "");

        throw new Exception($"Cannot apply '-' to values of types {left?.GetType()} and {right?.GetType()}");
    }

    public override object? VisitMultiplicativeExpression([NotNull] iLangParser.MultiplicativeExpressionContext context)
    {
        var left = Visit(context.expression(0));
        var right = Visit(context.expression(1));

        var op = context.multOp().GetText();

        return op switch
        {
            "*" => Multiply(left, right),
            "/" => Divide(left, right),
            "%" => Modulate(left, right),
            _ => throw new NotImplementedException($"operator '{op}' is not defined.")
        };
    }
    private object? Multiply(object? left, object? right)
    {

        if (left is int l && right is int r)
            return l * r;

        if (left is float fl && right is float fr)
            return fl * fr;

        if (left is int lInt && right is float rFloat)
            return lInt * rFloat;

        if (left is float flFloat && right is int frInt)
            return flFloat * frInt;

        if (left is string ls && right is int ri)
            return $"{String.Concat(Enumerable.Repeat(ls, ri))}";

        if (right is string rs && left is int li)
            return $"{String.Concat(Enumerable.Repeat(rs, li))}";

        throw new Exception($"Cannot apply '*' to values of types {left?.GetType()} and {right?.GetType()}");
    }
    private object? Divide(object? left, object? right)
    {
        if (right is int NI)
            if (NI == 0) throw new Exception("Cannot Devide by 0");
        if (right is float NF)
            if (NF == 0) throw new Exception("Cannot Devide by 0");

        if (left is int l && right is int r)
            return l / r;

        if (left is float fl && right is float fr)
            return fl / fr;

        if (left is int lInt && right is float rFloat)
            return lInt / rFloat;

        if (left is float flFloat && right is int frInt)
            return flFloat / frInt;

        throw new Exception($"Cannot apply '/' to values of types {left?.GetType()} and {right?.GetType()}");
    }
    private object? Modulate(object? left, object? right)
    {

        if (right is int NI)
            if (NI == 0) throw new Exception("Cannot Devide by 0");
        if (right is float NF)
            if (NF == 0) throw new Exception("Cannot Devide by 0");

        if (left is int l && right is int r)
            return l % r;

        if (left is float fl && right is float fr)
            return fl % fr;

        if (left is int lInt && right is float rFloat)
            return lInt % rFloat;

        if (left is float flFloat && right is int frInt)
            return flFloat % frInt;

        throw new Exception($"Cannot apply '%' to values of types {left?.GetType()} and {right?.GetType()}");
    }

    public override object? VisitWhileBlock([NotNull] iLangParser.WhileBlockContext context)
    {
        Func<object?, bool> condition = context.WHILE().GetText() == "while" ? isTrue : isFalse;

        if (condition(Visit(context.expression())))
        {
            while (condition(Visit(context.expression())))
                Visit(context.block());
        }else
        {
            Visit(context.elseIfBlock());
        }

        return null;
    }
    public override object? VisitIfBlock([NotNull] iLangParser.IfBlockContext context)
    {
        Func<object?, bool> condition = isTrue;

        if (condition(Visit(context.expression())))
        {
            Visit(context.block());
        }
        else
        {
            Visit(context.elseIfBlock());
        }

        return null;
    }

    public override object VisitBooleanExpression([NotNull] iLangParser.BooleanExpressionContext context)
    {

        var left = Visit(context.expression(0));
        var right = Visit(context.expression(1));

        var op = context.boolOp().GetText();

        return op switch
        {
            "and" => IsAnd(left, right),
            "or" => IsOr(left, right),
            "xor" => IsXor(left, right),
            _ => throw new NotImplementedException()
        };
    }

    private bool IsAnd(object left, object right)
    {
        if(left is bool l && right is bool r)
            return l && r;
        throw new Exception($"Cannot compare values of types {left.GetType()} and {right.GetType()}");
    }

    private bool IsOr(object left, object right)
    {
        if (left is bool l && right is bool r)
            return l || r;
        throw new Exception($"Cannot compare values of types {left.GetType()} and {right.GetType()}");
    }

    private bool IsXor(object left, object right)
    {
        if (left is bool l && right is bool r)
            return l ^ r;
        throw new Exception($"Cannot compare values of types {left.GetType()} and {right.GetType()}");
    }

    public override object? VisitComparisonExpression( iLangParser.ComparisonExpressionContext context)
    {
        var left = Visit(context.expression(0));
        var right = Visit(context.expression(1));

        var op = context.compareOp().GetText();

        return op switch
        {
            "==" => IsEquals(left, right),
            "!=" => IsUnEqual(left, right),
            ">" => IsGreaterThan(left, right),
            "<" => IsLessThan(left, right),
            ">=" => IsGreaterOrEqualTo(left, right),
            "<=" => IsLessOrEqualTo(left, right),
            _ => throw new NotImplementedException()
        };
    }

    private bool IsEquals(object? left, object? right)
    {
        if(left is int l && right is int r) return l == r;
        if(left is  float lf && right is float rf)  return lf == rf;
        if(left is int li && right is float rF) return li == rF;
        if(left is float lF && right is int rI) return lF == rI;

        if(left is bool lB && right is bool rB) return lB == rB;
        if(left is string lS && right is string rS) return lS == rS;

        throw new Exception($"Cannot compare values of types {left.GetType()} and {right.GetType()}");
    }
    private bool IsUnEqual(object? left, object? right)
    {
        return !IsEquals(left, right);
    }
    private bool IsGreaterThan(object? left, object? right)
    {
        if (left is int l && right is int r) return l > r;
        if (left is float lf && right is float rf) return lf > rf;
        if (left is int li && right is float rF) return li > rF;
        if (left is float lF && right is int rI) return lF > rI;

        throw new Exception($"Cannot compare values of types {left.GetType()} and {right.GetType()}");
    }
    private bool IsLessThan(object? left, object? right)
    {
        if (left is int l && right is int r) return l < r;
        if (left is float lf && right is float rf) return lf < rf;
        if (left is int li && right is float rF) return li < rF;
        if (left is float lF && right is int rI) return lF < rI;

        throw new Exception($"Cannot compare values of types {left.GetType()} and {right.GetType()}");
    }
    private bool IsGreaterOrEqualTo(object? left, object? right)
    {
        if (left is int l && right is int r) return l >= r;
        if (left is float lf && right is float rf) return lf >= rf;
        if (left is int li && right is float rF) return li >= rF;
        if (left is float lF && right is int rI) return lF >= rI;

        throw new Exception($"Cannot compare values of types {left.GetType()} and {right.GetType()}");
    }
    private bool IsLessOrEqualTo(object? left, object? right)
    {
        if (left is int l && right is int r) return l <= r;
        if (left is float lf && right is float rf) return lf <= rf;
        if (left is int li && right is float rF) return li <= rF;
        if (left is float lF && right is int rI) return lF <= rI;

        throw new Exception($"Cannot compare values of types {left.GetType()} and {right.GetType()}");
    }






    //================================================//
    private bool isTrue(object? value)
    {
        if (value is bool b)
            return b;
        throw new Exception(value + " is not a boolean");
    }
    private bool isFalse(object? value) => !isTrue(value);
}