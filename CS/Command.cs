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

using System;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Events;
using Autodesk.Revit.ApplicationServices;

namespace Revit.SDK.Samples.StairsAutomation.CS
{
  /// <summary>
  /// Implement the Revit add-in IExternalDBApplication interface
  /// </summary>
  public class DbApp : IExternalDBApplication
  {
    string _model_path = "C:/a/vs/StairsAutomation/CS/Stairs_automation_2019_1.rvt";

    /// <summary>
    /// The implementation of the automatic stairs creation.
    /// </summary>
    public void Execute( Document document )
    {
      //UIDocument activeDocument = commandData.Application.ActiveUIDocument;
      //Document document = activeDocument.Document;

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
      object sender, 
      ApplicationInitializedEventArgs e )
    {
      // Sender is an Application instance:

      Application app = sender as Application;

      Document doc = app.OpenDocumentFile( _model_path );

      if( doc == null )
      {
        throw new InvalidOperationException(
          "Could not open document." );
      }
      Execute( doc );
    }

    public ExternalDBApplicationResult OnStartup( 
      ControlledApplication a )
    {
      // ApplicationInitialized cannot be used in Forge!
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

