//
// (C) Copyright 2003-2017 by Autodesk, Inc.
//
// Permission to use, copy, modify, and distribute this software in
// object code form for any purpose and without fee is hereby granted,
// provided that the above copyright notice appears in all copies and
// that both that copyright notice and the limited warranty and
// restricted rights notice below appear in all supporting
// documentation.
//
// AUTODESK PROVIDES THIS PROGRAM "AS IS" AND WITH ALL FAULTS.
// AUTODESK SPECIFICALLY DISCLAIMS ANY IMPLIED WARRANTY OF
// MERCHANTABILITY OR FITNESS FOR A PARTICULAR USE. AUTODESK, INC.
// DOES NOT WARRANT THAT THE OPERATION OF THE PROGRAM WILL BE
// UNINTERRUPTED OR ERROR FREE.
//
// Use, duplication, or disclosure by the U.S. Government is subject to
// restrictions set forth in FAR 52.227-19 (Commercial Computer
// Software - Restricted Rights) and DFAR 252.227-7013(c)(1)(ii)
// (Rights in Technical Data and Computer Software), as applicable.
//

using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Events;

namespace Revit.SDK.Samples.StairsAutomation.CS
{
  /// <summary>
  /// Implement the Revit add-in IExternalDBApplication interface
  /// </summary>
  [Transaction( TransactionMode.Manual )]
  public class App : IExternalDBApplication
  {
    /// <summary>
    /// The implementation of the automatic stairs creation.
    /// </summary>
    public void Execute()
    {
      //UIDocument activeDocument = commandData.Application.ActiveUIDocument;
      Document document = null; // activeDocument.Document;

      // Create an automation utility with a hardcoded 
      // stairs configuration number

      StairsAutomationUtility utility 
        = StairsAutomationUtility.Create( 
          document, stairsConfigs[stairsIndex] );

      // Generate the stairs

      utility.GenerateStairs();

      ++stairsIndex;
      if( stairsIndex > 4 )
        stairsIndex = 0;
    }

    void OnApplicationInitialized( 
      object sender, ApplicationInitializedEventArgs e )
    {
      Execute();
    }

    public ExternalDBApplicationResult OnStartup( 
      ControlledApplication a )
    {
      a.ApplicationInitialized += OnApplicationInitialized;
      return ExternalDBApplicationResult.Succeeded;
    }

    public ExternalDBApplicationResult OnShutdown( 
      ControlledApplication a )
    {
      return ExternalDBApplicationResult.Succeeded;
    }

    private static int stairsIndex = 0;
    private static int[] stairsConfigs = { 0, 3, 4, 1, 2 };
  }
}

