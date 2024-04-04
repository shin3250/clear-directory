using System;
using System.IO;

namespace clear_directory {
    class Program {
        static int Main(string[] args) {
#if DEBUG
            args = new string[] { @"C:\Users\fbj04244.DC00\AppData\Local\Microsoft\Windows\INetCache\Content.Outlook" };
#endif
            if (args.Length != 1) {
                Console.WriteLine("対象ディレクトリを指定してください。");
                return -1;
            }

            if (!Directory.Exists(args[0])) {
                Console.WriteLine("対象ディレクトリが存在しません。");
                return -1;
            }

            return DeleteAllSubs(args[0], false);
        }

        static int DeleteAllSubs(string dir, bool ownFlag = true) {
            foreach (var subFIles in Directory.GetFiles(dir, "*.*", SearchOption.TopDirectoryOnly)) {
                try {
                    var fileInfo = new FileInfo(subFIles);
                    if ((fileInfo.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly) {
                        fileInfo.Attributes &= ~FileAttributes.ReadOnly;
                    }
                    if ((fileInfo.Attributes & FileAttributes.System) == FileAttributes.System) {
                        fileInfo.Attributes &= ~FileAttributes.System;
                    }
                    fileInfo.Delete();
                } catch { }
            }

            foreach (var subDir in Directory.GetDirectories(dir, "*.*", SearchOption.TopDirectoryOnly)) {
                DeleteAllSubs(subDir);
            }

            if (ownFlag) {
                try {
                    Directory.Delete(dir);
                } catch { }
            }

            return 0;
        }
    }
}
