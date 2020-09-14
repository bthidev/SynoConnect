namespace SynoConnect.Back.Models
{
    static class StatusTask
    {
        static string Waiting { get => "waiting"; }
        static string Downloading { get => "downloading"; }
        static string Paused { get => "paused"; }
        static string Finishing { get => "finishing"; }
        static string Finished { get => "finished"; }
        static string HashCheck { get => "hash_checking"; }
        static string Seeding { get => "seeding"; }
        static string FileHostWait { get => "filehosting_waiting"; }
        static string Extracting { get => "extracting"; }
        static string Error { get => "error"; }
    }
}
