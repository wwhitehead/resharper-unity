﻿using JetBrains.ReSharper.FeaturesTestFramework.Intentions;
using JetBrains.ReSharper.Plugins.Unity.Feature.Services.QuickFixes;
using NUnit.Framework;

namespace JetBrains.ReSharper.Plugins.Unity.Tests.Intentions.QuickFixes
{
    [TestUnity]
    public class InvalidReturnTypeFixAvailabilityTests : 
        QuickFixAvailabilityTestBase
    {
        protected override string RelativeTestDataPath=> @"Intentions\QuickFixes\InvalidReturnType\Availability";

        [Test] public void Test01() { DoNamedTest(); }
        [Test] public void Test02() { DoNamedTest(); }
        [Test] public void Test03() { DoNamedTest(); }
    }

    [TestUnity]
    public class InvalidReturnTypeFixTests : CSharpQuickFixTestBase<InvalidReturnTypeFix>
    {
        protected override string RelativeTestDataPath=> @"Intentions\QuickFixes\InvalidReturnType";

        [Test] public void Test01() { DoNamedTest(); }
        [Test] public void Test02() { DoNamedTest(); }
        [Test] public void Test03() { DoNamedTest(); }
    }
}