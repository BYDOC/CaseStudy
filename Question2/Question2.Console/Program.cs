using Question2.Console;


Console.WriteLine("Hello, World!");
string file = Path.Combine(AppContext.BaseDirectory, "OcrOutputs", "response.json");
string x = ReceiptParser.Build(file);
Console.WriteLine(x);

