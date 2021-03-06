﻿using System;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.Resolve;
using JetBrains.ReSharper.Psi.Tree;

namespace JetBrains.ReSharper.Plugins.Unity.Psi.Resolve
{
    [ReferenceProviderFactory]
    public class UnityEventFunctionReferenceProviderFactory : IReferenceProviderFactory
    {
        public IReferenceFactory CreateFactory(IPsiSourceFile sourceFile, IFile file)
        {
            var project = sourceFile.GetProject();
            if (project == null || !project.IsUnityProject())
                return null;

            if (sourceFile.PrimaryPsiLanguage.Is<CSharpLanguage>())
                return new UnityEventFunctionReferenceFactory();

            return null;
        }

        public event Action OnChanged;
    }
}