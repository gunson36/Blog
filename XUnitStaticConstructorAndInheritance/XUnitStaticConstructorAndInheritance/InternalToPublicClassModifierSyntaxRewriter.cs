﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace XUnitStaticConstructorAndInheritance
{
    public class InternalToPublicClassModifierSyntaxRewriter : CSharpSyntaxRewriter
    { 
        public override SyntaxNode VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            var internalModifierIndex = node.Modifiers.IndexOf(SyntaxKind.InternalKeyword);
            
            if (internalModifierIndex > -1)
            {
                var tokenInList = node.Modifiers[internalModifierIndex];
                var updatedModifiers = node.Modifiers.Replace(tokenInList,
                    Token(SyntaxKind.PublicKeyword).WithLeadingTrivia(tokenInList.LeadingTrivia)
                        .WithTrailingTrivia(tokenInList.TrailingTrivia));

                node = node.WithModifiers(updatedModifiers);
            }

            return base.VisitClassDeclaration(node);
        }
    }
}