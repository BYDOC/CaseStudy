using Question1.Console;

int count = 1000;
var codes = CodeGenerator.Generate();

for (int i = 0; i < count; i++)
{
    Console.WriteLine(CodeGenerator.Generate() +":" +(CodeGenerator.IsValid(CodeGenerator.Generate()) ? " (valid)" : " (invalid)"));
}

while (true)
{

    Console.WriteLine("Enter a key to check its validity:");
    var key = Console.ReadLine();
    Console.WriteLine(CodeGenerator.IsValid(key) ? " (valid)" : " (invalid)");


}
