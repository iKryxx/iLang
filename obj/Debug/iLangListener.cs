﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.6.6
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:\Users\maiks\Documents\JanikGames\Own Language\Content\iLang.g4 by ANTLR 4.6.6

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace Own_Language {
using Antlr4.Runtime.Misc;
using IParseTreeListener = Antlr4.Runtime.Tree.IParseTreeListener;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete listener for a parse tree produced by
/// <see cref="iLangParser"/>.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.6.6")]
[System.CLSCompliant(false)]
public interface IiLangListener : IParseTreeListener {
	/// <summary>
	/// Enter a parse tree produced by the <c>constantExpression</c>
	/// labeled alternative in <see cref="iLangParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterConstantExpression([NotNull] iLangParser.ConstantExpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>constantExpression</c>
	/// labeled alternative in <see cref="iLangParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitConstantExpression([NotNull] iLangParser.ConstantExpressionContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>identifierExpression</c>
	/// labeled alternative in <see cref="iLangParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIdentifierExpression([NotNull] iLangParser.IdentifierExpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>identifierExpression</c>
	/// labeled alternative in <see cref="iLangParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIdentifierExpression([NotNull] iLangParser.IdentifierExpressionContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>functionCallExpression</c>
	/// labeled alternative in <see cref="iLangParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFunctionCallExpression([NotNull] iLangParser.FunctionCallExpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>functionCallExpression</c>
	/// labeled alternative in <see cref="iLangParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFunctionCallExpression([NotNull] iLangParser.FunctionCallExpressionContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>parenthesizedExpression</c>
	/// labeled alternative in <see cref="iLangParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterParenthesizedExpression([NotNull] iLangParser.ParenthesizedExpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>parenthesizedExpression</c>
	/// labeled alternative in <see cref="iLangParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitParenthesizedExpression([NotNull] iLangParser.ParenthesizedExpressionContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>notExpression</c>
	/// labeled alternative in <see cref="iLangParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNotExpression([NotNull] iLangParser.NotExpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>notExpression</c>
	/// labeled alternative in <see cref="iLangParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNotExpression([NotNull] iLangParser.NotExpressionContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>multiplicativeExpression</c>
	/// labeled alternative in <see cref="iLangParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterMultiplicativeExpression([NotNull] iLangParser.MultiplicativeExpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>multiplicativeExpression</c>
	/// labeled alternative in <see cref="iLangParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitMultiplicativeExpression([NotNull] iLangParser.MultiplicativeExpressionContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>additiveExpression</c>
	/// labeled alternative in <see cref="iLangParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAdditiveExpression([NotNull] iLangParser.AdditiveExpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>additiveExpression</c>
	/// labeled alternative in <see cref="iLangParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAdditiveExpression([NotNull] iLangParser.AdditiveExpressionContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>comparisonExpression</c>
	/// labeled alternative in <see cref="iLangParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterComparisonExpression([NotNull] iLangParser.ComparisonExpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>comparisonExpression</c>
	/// labeled alternative in <see cref="iLangParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitComparisonExpression([NotNull] iLangParser.ComparisonExpressionContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>booleanExpression</c>
	/// labeled alternative in <see cref="iLangParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBooleanExpression([NotNull] iLangParser.BooleanExpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>booleanExpression</c>
	/// labeled alternative in <see cref="iLangParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBooleanExpression([NotNull] iLangParser.BooleanExpressionContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="iLangParser.program"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterProgram([NotNull] iLangParser.ProgramContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="iLangParser.program"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitProgram([NotNull] iLangParser.ProgramContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="iLangParser.line"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLine([NotNull] iLangParser.LineContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="iLangParser.line"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLine([NotNull] iLangParser.LineContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="iLangParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStatement([NotNull] iLangParser.StatementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="iLangParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStatement([NotNull] iLangParser.StatementContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="iLangParser.ifBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIfBlock([NotNull] iLangParser.IfBlockContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="iLangParser.ifBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIfBlock([NotNull] iLangParser.IfBlockContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="iLangParser.elseIfBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterElseIfBlock([NotNull] iLangParser.ElseIfBlockContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="iLangParser.elseIfBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitElseIfBlock([NotNull] iLangParser.ElseIfBlockContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="iLangParser.whileBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterWhileBlock([NotNull] iLangParser.WhileBlockContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="iLangParser.whileBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitWhileBlock([NotNull] iLangParser.WhileBlockContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="iLangParser.assignment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAssignment([NotNull] iLangParser.AssignmentContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="iLangParser.assignment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAssignment([NotNull] iLangParser.AssignmentContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="iLangParser.function"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFunction([NotNull] iLangParser.FunctionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="iLangParser.function"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFunction([NotNull] iLangParser.FunctionContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="iLangParser.functionCall"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFunctionCall([NotNull] iLangParser.FunctionCallContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="iLangParser.functionCall"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFunctionCall([NotNull] iLangParser.FunctionCallContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="iLangParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpression([NotNull] iLangParser.ExpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="iLangParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpression([NotNull] iLangParser.ExpressionContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="iLangParser.functionExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFunctionExpression([NotNull] iLangParser.FunctionExpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="iLangParser.functionExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFunctionExpression([NotNull] iLangParser.FunctionExpressionContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="iLangParser.multOp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterMultOp([NotNull] iLangParser.MultOpContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="iLangParser.multOp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitMultOp([NotNull] iLangParser.MultOpContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="iLangParser.addOp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAddOp([NotNull] iLangParser.AddOpContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="iLangParser.addOp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAddOp([NotNull] iLangParser.AddOpContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="iLangParser.compareOp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterCompareOp([NotNull] iLangParser.CompareOpContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="iLangParser.compareOp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitCompareOp([NotNull] iLangParser.CompareOpContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="iLangParser.boolOp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBoolOp([NotNull] iLangParser.BoolOpContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="iLangParser.boolOp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBoolOp([NotNull] iLangParser.BoolOpContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="iLangParser.constant"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterConstant([NotNull] iLangParser.ConstantContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="iLangParser.constant"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitConstant([NotNull] iLangParser.ConstantContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="iLangParser.block"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBlock([NotNull] iLangParser.BlockContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="iLangParser.block"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBlock([NotNull] iLangParser.BlockContext context);
}
} // namespace Own_Language
