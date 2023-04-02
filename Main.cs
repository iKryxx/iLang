using Antlr4.Runtime;
using Own_Language;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

bool runInTest = (args == null || args.Length == 0);

if(runInTest) Console.WriteLine("This is a Test Run becuase no arguments were passed when executing the program.");

var fileName = runInTest ? "Content/test.iks" : args[0];

var fileContents = File.ReadAllText(fileName);

var inputStream = new AntlrInputStream(fileContents);

var iLankLexer = new iLangLexer(inputStream);

var commonTokenStream = new CommonTokenStream(iLankLexer);

var iLangparser = new iLangParser(commonTokenStream);

//iLangparser.AddErrorListener(new IAntlrErrorListener(in var s));

var programmContext = iLangparser.program();

var visitor = new iLangVisitor();

visitor.Visit(programmContext);

Console.ReadKey();
