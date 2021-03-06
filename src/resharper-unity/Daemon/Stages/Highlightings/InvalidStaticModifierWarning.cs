﻿using JetBrains.DocumentModel;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Plugins.Unity.Daemon.Stages.Highlightings;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Parsing;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;

[assembly: RegisterConfigurableSeverity(InvalidStaticModifierWarning.HIGHLIGHTING_ID,
    null, UnityHighlightingGroupIds.Unity, InvalidStaticModifierWarning.MESSAGE,
    "Incorrect static modifier for Unity event function.",
    Severity.WARNING)]

namespace JetBrains.ReSharper.Plugins.Unity.Daemon.Stages.Highlightings
{
    // TODO: Check out CSharpConflictDeclarationsContextSearch
    // Note that we don't use ToolTipFormatString here. This specifies a
    // string format like template that is used to reverse engineer the
    // arguments so that SWEA can optimise and store only the arguments,
    // and not the entire string
    [ConfigurableSeverityHighlighting(HIGHLIGHTING_ID, CSharpLanguage.Name,
        OverlapResolve = OverlapResolveKind.WARNING)]
    public class InvalidStaticModifierWarning : IHighlighting, IUnityHighlighting
    {
        public const string HIGHLIGHTING_ID = "Unity.InvalidStaticModifier";
        public const string MESSAGE = "Incorrect static modifier for Unity event function";

        public InvalidStaticModifierWarning(IMethodDeclaration methodDeclaration, UnityEventFunction function)
        {
            MethodDeclaration = methodDeclaration;
            Function = function;
        }

        public bool IsValid()
        {
            return MethodDeclaration != null && MethodDeclaration.IsValid();
        }

        public DocumentRange CalculateRange()
        {
            var nameRange = MethodDeclaration.GetNameDocumentRange();
            if (!nameRange.IsValid())
                return DocumentRange.InvalidRange;

            if (MethodDeclaration.IsStatic)
            {
                var modifiersList = MethodDeclaration.ModifiersList;
                foreach (var modifier in modifiersList.Modifiers)
                {
                    if (modifier.NodeType == CSharpTokenType.STATIC_KEYWORD)
                        return modifier.GetDocumentRange();
                }

                var modifiersListRange = modifiersList.GetDocumentRange();
                if (modifiersListRange.IsValid() && !modifiersListRange.IsEmpty)
                    return modifiersListRange;
            }

            return nameRange;
        }

        public string ToolTip => MethodDeclaration.IsStatic ? MESSAGE : "Missing static modifier for Unity event function";

        public string ErrorStripeToolTip => ToolTip;

        public IMethodDeclaration MethodDeclaration { get; }
        public UnityEventFunction Function { get; }
    }
}