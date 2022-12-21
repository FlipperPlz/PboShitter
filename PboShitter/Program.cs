using BisUtils.PBO;
using BisUtils.PBO.Builders;

if (args.Length == 0 | string.IsNullOrEmpty(args[0]) | !File.Exists(args[0])) Error();

var pbo = new PboFile(args[0], PboFileOption.Read);

for (var i = 0; i < 5; i++) {
    pbo.GetVersionEntry()!.AddMetadataProperty("obfuscated", true.ToString(), false);
    pbo.GetVersionEntry()!.AddMetadataProperty("sus", "ඞ", false);
}

foreach (var entry in pbo.GetDataEntries()) {
    pbo.AddEntry(new PboDataEntryDto(pbo, Stream.Null, UInt64.MaxValue, true) {
        EntryName = entry.EntryName
    });
    pbo.AddEntry(new PboDataEntryDto(pbo, Stream.Null, UInt64.MaxValue, true) {
        EntryName = $"{entry.EntryName}\\{entry.EntryName}"
    });
    pbo.AddEntry(new PboDataEntryDto(pbo, Stream.Null, UInt64.MaxValue, true) {
        EntryName = $"{entry.EntryName}\\{entry.EntryName}"
    });
}

pbo.SynchronizeStream();

void Error() {
    Console.WriteLine("You dont know how to use this program correctly.");
    Environment.Exit(1);
}