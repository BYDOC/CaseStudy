using Question2.Console;

string file = Path.Combine(AppContext.BaseDirectory, "OcrOutputs", "response.json");
Console.WriteLine(ReceiptParser.Build(file));

