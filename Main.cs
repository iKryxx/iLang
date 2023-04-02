using Antlr4.Runtime;
using Own_Language;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



var fileName = "Content/test.iks";

var fileContents = File.ReadAllText(fileName);

var inputStream = new AntlrInputStream(fileContents);

var iLankLexer = new iLangLexer(inputStream);

var commonTokenStream = new CommonTokenStream(iLankLexer);

var iLangparser = new iLangParser(commonTokenStream);

//iLangparser.AddErrorListener(new IAntlrErrorListener(in var s));

var programmContext = iLangparser.program();

var visitor = new iLangVisitor();

visitor.Visit(programmContext);
