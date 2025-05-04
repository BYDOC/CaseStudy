using Question1.Console;

int count = 1_000_000;

CodeGenerator.CheckDuplicates(count);
while (true)
{
    Console.WriteLine("Enter a key to check its validity:");
    var key = Console.ReadLine();
    Console.WriteLine(CodeGenerator.IsValid(key) ? " (valid)" : " (invalid)");
}


