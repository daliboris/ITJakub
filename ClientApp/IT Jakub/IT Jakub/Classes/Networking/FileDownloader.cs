﻿using IT_Jakub.Classes.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage;

namespace IT_Jakub.Classes.Networking {
    class FileDownloader {


        public static async Task<StorageFile> downloadFileFromUri(Uri source) {

            string fileext = Path.GetExtension(source.ToString());
            string destination = ("tempFile" + fileext).Trim();

            if (string.IsNullOrWhiteSpace(destination)) {
                // rootPage.NotifyUser("A local file name is required.", NotifyType.ErrorMessage);
                return null;
            }

            StorageFile destinationFile;
            try {
                var applicationData = Windows.Storage.ApplicationData.Current;
                var temporaryFolder = applicationData.TemporaryFolder;
                destinationFile = await temporaryFolder.CreateFileAsync(
                    destination, CreationCollisionOption.GenerateUniqueName);
            } catch (FileNotFoundException ex) {
                throw new MyFileNotFoundException(ex);
            }

            BackgroundDownloader downloader = new BackgroundDownloader();
            DownloadOperation download = downloader.CreateDownload(source, destinationFile);
            await download.StartAsync();
            object ob = destinationFile;
            return destinationFile;
        }

        public static async void clearTempFolder() {
            var applicationData = Windows.Storage.ApplicationData.Current;
            var temporaryFolder = applicationData.TemporaryFolder;
            try {
                await temporaryFolder.DeleteAsync();
            } catch (Exception e) {
                object o = e;
            }
        }
    }
}
