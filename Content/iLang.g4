grammar iLang;

program: line* EOF; //this defines how a program is built (lines, * is saying 0 or more, and an End of File)

line: statement | ifBlock | whileBlock; //whats a Line? its either a statement, an ifBlock or a whileBlock (| is the syntax for 'or'

statement: (assignment | functionCall | functionAssignment) ';'; //whats a statement? its either an assignment or a function Call, followed by a semicolon

ifBlock: 'if' expression block ('else' elseIfBlock)?; //whats an ifBlock? its triggered when writing 'if' followed by the expression and the block. optionally (labeled with '?'), an 'else' can follow with an else block

elseIfBlock: block | ifBlock; //whats an elseIfBlock? its either a block (else {}) or another ifBlock (else if expression {})

whileBlock: WHILE expression block ('else' elseIfBlock)?;

WHILE: 'while' | 'until';

assignment: IDENTIFIER '=' expression;

functionAssignment: constant? IDENTIFIER '(' (functionExpression (',' functionExpression)*)? ')' block;

functionCall: IDENTIFIER '(' (expression (',' expression)*)? ')'; //consists of an Identifier that can hold zero or more expressions in them

expression
    : constant                              #constantExpression
    | IDENTIFIER                            #identifierExpression
    | functionCall                          #functionCallExpression
    | '(' expression ')'                    #parenthesizedExpression
    | '!' expression                        #notExpression
    | expression multOp expression          #multiplicativeExpression
    | expression addOp expression           #additiveExpression
    | expression compareOp expression       #comparisonExpression
    | expression boolOp expression          #booleanExpression
    ;

functionExpression
    : constant? IDENTIFIER
    ;

multOp: '*' | '/' | '%';
addOp: '+' | '-';
compareOp: '==' | '!=' | '>' | '<' | '>=' | '<=';
boolOp: BOOL_OPERATOR;

BOOL_OPERATOR: 'and' | 'or' | 'xor' ; 

constant: INTEGER | FLOAT | STRING | BOOL | NULL;

INTEGER: ([1-9][0-9]*) | [0];
FLOAT: ([1-9][0-9]* '.' [0-9]*)'f' | [0]'f';
STRING: ('"' ~'"'* '"') | ('\'' ~'\''* '\'');
BOOL: 'true' | 'false';
NULL: 'null';

block: '{' line* '}';

WS: [ \t\r\n]+ -> skip; //skip any whitespaces, tabs, carriage returns and newlines in the code (not in strings, dont worry)
IDENTIFIER: [a-zA-Z_][a-zA-Z0-9_]*; //identifiers must start with a letter or underscore, following by 0 or more letters, numbers or underscores

//asd
//but also more tooo