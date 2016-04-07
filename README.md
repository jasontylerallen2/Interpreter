# Interpreter

This project is an interpreter that employs an ad-hoc scanner and a recursive-descent parser. The parser builds a strongly typed parse tree, which is than traversed and evaluated.

See the grammar text file within the source to understand the language, and see examples of what can be interpreted within the test folder. To write a new test, simply create a new folder within the test folder, and include a file called "prg" that contains the program to interpret, and a file called "exp" that includes the expected result.
