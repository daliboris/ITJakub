﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Ujc.Ovj.Ooxml.Conversion.Test
{
	class Program
	{
		static void Main(string[] args)
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			MakeBulkConversion();
			//MakeConversion();
			Console.WriteLine("Conversion finished in {0}.", stopwatch.Elapsed);
			Console.ReadLine();
		}

		private static void MakeBulkConversion()
		{
			string dataDirectory = GetDataDirectory();
			string inputDirectory = Path.Combine(dataDirectory, "Input");
			string outputDirectory = Path.Combine(dataDirectory, "Output");
			DirectoryInfo inputDirectoryInfo = new DirectoryInfo(inputDirectory);
			FileInfo[] files = inputDirectoryInfo.GetFiles("*.docx", SearchOption.TopDirectoryOnly);
			foreach (FileInfo fileInfo in files)
			{
				//Příprava souborů do dočasné složky
				DirectoryInfo outputDirectoryInfo = new DirectoryInfo(Path.Combine(outputDirectory, Path.GetFileNameWithoutExtension(fileInfo.Name)));
				if (!outputDirectoryInfo.Exists)
					Directory.CreateDirectory(outputDirectoryInfo.FullName);
				File.Copy(fileInfo.FullName, Path.Combine(outputDirectoryInfo.FullName, fileInfo.Name), true);
				string metadataFilePath = Path.Combine(inputDirectory, "Evidence.xml");


				MakeConversion(outputDirectoryInfo.FullName, fileInfo.Name, metadataFilePath);

				//MakeConversion(dataDirectory, Path.GetFileNameWithoutExtension(fileInfo.Name));
			}
		}

		//Ukázka volání konverze
		//Pro DocxToTeiConverterSettings stačí 3 hlavní parametry: 
			//cesta ke konvertovanému souboru
			//cesta k souboru s metadaty
			//funkce/delegát pro načítání verzí souboru
		private static void MakeConversion(string conversionDirectoryFullName, string documentName, string metadataFilePath)
		{
			DocxToTeiConverterSettings settings = new DocxToTeiConverterSettings();
			settings.InputFilePath = Path.Combine(conversionDirectoryFullName, documentName);
			settings.MetadataFilePath = metadataFilePath;
			settings.GetVersionList = GetVersions();
			String.Format("Úprava souboru k {0:g}", DateTime.Now);
			DoConversion(settings);
		}


		private static void MakeConversion(string dataDirectory, string documentName)
		{
			DocxToTeiConverterSettings settings = new DocxToTeiConverterSettings();
			settings.TempDirectoryPath = Path.Combine(dataDirectory, "Temp");
			settings.MetadataFilePath = Path.Combine(dataDirectory, "Input", "Evidence.xml");
			settings.InputFilePath = Path.Combine(dataDirectory, "Input", documentName + ".docx");
			settings.OutputFilePath = Path.Combine(dataDirectory, "Output", documentName + ".xml");
			String.Format("Úprava souboru k {0:g}", DateTime.Now);
			settings.GetVersionList = GetVersions();
			DocxToTeiConverter converter = new DocxToTeiConverter();
			ConversionResult result = converter.Convert(settings);
			if (result.IsConverted)
			{
				Console.WriteLine("File {0} converted.", settings.InputFilePath);
			}
			else
			{
				Console.WriteLine("File {0} not converted.", settings.InputFilePath);
				Console.WriteLine("Errors: {0}", result.Errors);
			}
		}

		public static Func<string, List<VersionInfoSkeleton>> GetVersions()
		{
			List<VersionInfoSkeleton> versionList = new List<VersionInfoSkeleton>();
			versionList.Add(new VersionInfoSkeleton("Message 1", DateTime.UtcNow.AddDays(-2)));
			versionList.Add(new VersionInfoSkeleton("Message 2", DateTime.UtcNow));
			return s => versionList;
		}

		private static void MakeConversion()
		{
			string dataDirectory = GetDataDirectory();

			DocxToTeiConverterSettings settings = new DocxToTeiConverterSettings();

			settings.TempDirectoryPath = Path.Combine(dataDirectory, "Temp");
			settings.MetadataFilePath = Path.Combine(dataDirectory, "Input", "Evidence.xml");
			settings.InputFilePath = Path.Combine(dataDirectory, "Input", "AlexPovD.docx");
			settings.OutputFilePath = Path.Combine(dataDirectory, "Output", "AlexPovD.xml");
			String.Format("Úprava souboru k {0:g}", DateTime.Now);
			settings.SplitDocumentByPageBreaks = true;
			DoConversion(settings);
		}

		private static void DoConversion(DocxToTeiConverterSettings settings)
		{
			DocxToTeiConverter converter = new DocxToTeiConverter();
			ConversionResult result = converter.Convert(settings);
			if (result.IsConverted)
			{
				Console.WriteLine("File {0} converted.", settings.InputFilePath);
			}
			else
			{
				Console.WriteLine("File {0} not converted.", settings.InputFilePath);
				Console.WriteLine("Errors: {0}", result.Errors);
			}
		}

		private static string GetDataDirectory()
		{
			string dataDirectory = AssemblyDirectory;
			dataDirectory = dataDirectory.Substring(0,
				dataDirectory.LastIndexOf(String.Format("{0}bin{0}", Path.DirectorySeparatorChar)));

			dataDirectory = Path.Combine(dataDirectory, "Data");
			return dataDirectory;
		}

		public static string AssemblyDirectory
		{
			get
			{
				string codeBase = Assembly.GetExecutingAssembly().CodeBase;
				UriBuilder uri = new UriBuilder(codeBase);
				string path = Uri.UnescapeDataString(uri.Path);
				return Path.GetDirectoryName(path);
			}
		}
	}
}