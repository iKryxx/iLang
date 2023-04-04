using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Own_Language;

namespace iKryxxLanguage;
public class iLangVisitor : iLangBaseVisitor<object?>
{
    private Dictionary<string, object?> Variables { get; } = new();
    private iLangFunction currentFunction;
    object? returnValue;
    private static Dictionary<string, object?> LibFunctions { get; } = new();

    public iLangVisitor()
    {
        LibFunctions["Write"] = new Func<object?[], object?>(LibraryMethods.Write);
        LibFunctions["Random"] = new Func<object?[], object>(LibraryMethods.Random);
    }

   


    public override object VisitFunctionCall([NotNull] iLangParser.FunctionCallContext context)
    {
        var name = context.IDENTIFIER().GetText();
        var args = context.expression().Select(Visit).ToArray();
        Func<object?[], object?> func = null;
        iLangFunction func2 = null;


        if (LibFunctions.ContainsKey(name)) { func = (Func<object[], object>)LibFunctions[name]; }
        if (Variables.ContainsKey(name)) { func2 = (iLangFunction)Variables[name]; }



        if (func == null && func2 == null) throw new Exception($"{name} isn't defined");
        if(func != null) return func(args);
        if (func2 != null)
        {
            var body = func2.getContextByArguments(args.Length); if (body == null) return null;
            var expr = body.functionExpression().ToArray();
            //if (expr.Length != args.Length) throw new Exception($"'{name}' takes '{expr.Length}' arguments, passed were '{args.Length}'");
            for (int i = 0; i < expr.Length; i++)
            {                
                Variables[expr[i].IDENTIFIER().GetText()] =args[i];
            }
            currentFunction = (iLangFunction)Variables[name];
            Visit(body.block());
            if (currentFunction != null)
            {
                returnValue = currentFunction.returnValue;
                currentFunction.returnValue = null;
                currentFunction = null;
                
            }
            return returnValue;
        }   
        return null;
    }
    public override object VisitLine([NotNull] iLangParser.LineContext context)
    {
        if (abort && currentFunction == null) { abort = false; returnValue = null; }
        if (abort) return null;
        return base.VisitLine(context);
    }
    public override object VisitReturn([NotNull] iLangParser.ReturnContext context)
    {
        if(currentFunction != null) currentFunction.returnValue = Visit(context.expression());
        abort = true;
        return null;
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
    public override object VisitFunctionAssignment([NotNull] iLangParser.FunctionAssignmentContext context)
    {
        var funName = context.IDENTIFIER().GetText();
        object[] funArgs = context.functionExpression().Select(Visit).ToArray();

        if (LibFunctions.ContainsKey(funName)) throw new Exception("Cannot Overload or Override library functions");
        if (Variables.TryGetValue(funName, out var _func))
        {
            iLangFunction func = (iLangFunction) _func;
            foreach (var f in func.bodies)
            {
                if (f.functionExpression().ToArray().Length == funArgs.Length) throw new Exception(funName + " already takes an overload with " + funArgs.Length + " argument(s)");
            }
            iLangFunction _ = (iLangFunction)Variables[funName];
            _.AddContext(context);
        }
        else
        {
            iLangFunction _ =  new iLangFunction();
            _.AddContext(context);
            Variables[funName] = _;
        }
        return null;
    }
    public override object VisitFunctionExpression([NotNull] iLangParser.FunctionExpressionContext context)
    {
        var name = context.IDENTIFIER().GetText();
        Variables[name] = null;
        return null; 
    }
    public override object? VisitConstant([NotNull] iLangParser.ConstantContext context)
    {
        //Console.WriteLine(context.GetText());
        if (context.INT() is { } i) return int.Parse(i.GetText());
        if (context.FLOAT() is { } f) return float.Parse(f.GetText().Replace("f","").Replace(".",","));
        if (context.STRING() is { } s) return s.GetText().Substring(1, s.GetText().Length - 2);
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

        if (left is int l && right is int r)
            return l + r;

        if (left is float fl && right is float fr)
            return fl + fr;

        if (left is int lInt && right is float rFloat)
            return lInt + rFloat;

        if (left is float flFloat && right is int frInt)
            return flFloat + frInt;

        if (left is string || right is string)
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
        }
        else
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
            if(context.elseIfBlock() != null)
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
        if (left is bool l && right is bool r)
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

    public override object? VisitComparisonExpression(iLangParser.ComparisonExpressionContext context)
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
        if (left is int l && right is int r) return l == r;
        if (left is float lf && right is float rf) return lf == rf;
        if (left is int li && right is float rF) return li == rF;
        if (left is float lF && right is int rI) return lF == rI;

        if (left is bool lB && right is bool rB) return lB == rB;
        if (left is string lS && right is string rS) return lS == rS;

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

    public static bool abort;
    

    public class iLangFunction
    {
        public List<iLangParser.FunctionAssignmentContext> bodies = new List<iLangParser.FunctionAssignmentContext>();
        public object? returnValue = null;
        public iLangFunction()
        {
            this.bodies = new List<iLangParser.FunctionAssignmentContext>();
        }
        public void AddContext(iLangParser.FunctionAssignmentContext context)
        {
            this.bodies.Add(context);
        }

        public iLangParser.FunctionAssignmentContext getContextByArguments(int argumentCount)
        {
            foreach (iLangParser.FunctionAssignmentContext body in bodies)
            {
                if(body.functionExpression().ToArray().Length == argumentCount)
                {
                    return body;
                }
            }
            throw new Exception("function doesnt have an Overload containing " + argumentCount + " arguments");
        }
    }

    public static class LibraryMethods
    {
        public static object? Write(object?[] args)
        {
            foreach (var arg in args)
            {
                Console.Write(arg);
            }
            Console.WriteLine();
            return null;
        }

        public static object? Random(object?[] args)
        {
            int precision = 4;

            object min = args[0];
            object max = args[1];
            bool minint;
            bool maxint;
            try
            {
                int ai =  min.ToString().Contains(",")? int.Parse(min.ToString().Remove(min.ToString().IndexOf(","))) : int.Parse(min.ToString());
                float af = float.Parse(min.ToString());
                int bi = max.ToString().Contains(",") ? int.Parse(max.ToString().Remove(max.ToString().IndexOf(","))) : int.Parse(max.ToString());
                float bf = float.Parse(max.ToString());
                
            
                minint = ai * 10 == (int)(af * 10) ? false : true;
                maxint = bi * 10 == (int)(bf * 10) ? false : true;
            }
            catch { throw new Exception($"Cannot execute Function on '{min.ToString()}' and '{max.ToString()}'"); }
            Type target = min.GetType();
            if (!maxint && maxint) throw new Exception($"Cannot execute Function on {max.ToString()}");
            if (target == typeof(int))
            {
                System.Random random = new Random();
                return random.Next((int)min, (int)max + 1);
            } else if(target == typeof(float))
            { 
                Random random = new Random();

                int lint = (int)Math.Round((float)min * Math.Pow(10, precision) - 0.5);
                int rint = (int)Math.Round((float)max * Math.Pow(10, precision) - 0.5);
                float result = random.Next(lint, rint);
                return result / Math.Pow(10,precision);
            }
            return 0;
        }      
    }
}