
// ReSharper disable MemberCanBePrivate.Global
namespace Utilities.File
{
    public readonly struct FileSize
    {
        public FileSize(long bytes)
        {
            Bytes = bytes;
        }

        public long Bytes { get; init; }
        public long KiloBytes => Bytes / 1024;
        public long MegaBytes => KiloBytes / 1024;
        public long GigaBytes => MegaBytes / 1024;
        public long TeraBytes => GigaBytes / 1024;
        public long PetaBytes => TeraBytes / 1024;
        public long ExaBytes => PetaBytes / 1024;

        public override string ToString()
        {
            return
                $"{Bytes} bytes ({KiloBytes} KB, {MegaBytes} MB, {GigaBytes} GB, {TeraBytes} TB, {PetaBytes} PB, {ExaBytes} EB)";

        }

        public static implicit operator FileSize(int bytes)
        {
            return new FileSize(bytes);
        }

        public static implicit operator FileSize(long bytes)
        {
            return new FileSize(bytes);
        }

        public static FileSize FromKb(double value) => new ((long)(value * 1024));
        public static FileSize FromMb(double value) => new ((long)(value * 1024 * 1024));
        public static FileSize FromGb(double value) => new ((long)(value * 1024 * 1024 * 1024));

        public static FileSize FromString(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(value));

            value = value.Trim();
            var units = new[] { "KB", "MB", "GB", "Kb", "Mb", "Gb" };
            var unit = units.FirstOrDefault(u => value.EndsWith(u, StringComparison.OrdinalIgnoreCase));
            if (unit == null)
                throw new ArgumentException("Value must end with KB, MB, or GB (case-insensitive).", nameof(value));

            var numberPart = value[..^unit.Length].Trim();
            if (!double.TryParse(numberPart, out var number))
                throw new ArgumentException("Invalid numeric value.", nameof(value));

            var bytes = unit.ToUpperInvariant() switch
            {
                "KB" => (long)(number * 1024),
                "MB" => (long)(number * 1024 * 1024),
                "GB" => (long)(number * 1024 * 1024 * 1024),
                _ => throw new ArgumentException("Unsupported unit.", nameof(value))
            };

            return new FileSize(bytes);
        }

    }
}
