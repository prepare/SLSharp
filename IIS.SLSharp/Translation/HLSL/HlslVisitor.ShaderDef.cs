﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ICSharpCode.Decompiler.Ast;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using ICSharpCode.NRefactory.TypeSystem.Implementation;
using IIS.SLSharp.Shaders;
using Mono.Cecil;
using Expression = ICSharpCode.NRefactory.CSharp.Expression;
using InvocationExpression = ICSharpCode.NRefactory.CSharp.InvocationExpression;
using LambdaExpression = ICSharpCode.NRefactory.CSharp.LambdaExpression;

namespace IIS.SLSharp.Translation.HLSL
{
    internal sealed partial class HlslVisitor
    {
        public HlslVisitor()
        {
            Handlers = new Dictionary<Expression<Action>, Func<MethodDefinition, InvocationExpression, StringBuilder>>(new HandlerComparer())
            {
                #region Trigonometry

                { () => ShaderDefinition.Radians(_float), ToLower },
                { () => ShaderDefinition.Radians(vec2), ToLower },
                { () => ShaderDefinition.Radians(vec3), ToLower },
                { () => ShaderDefinition.Radians(vec4), ToLower },

                { () => ShaderDefinition.Degrees(_float), ToLower },
                { () => ShaderDefinition.Degrees(vec2), ToLower },
                { () => ShaderDefinition.Degrees(vec3), ToLower },
                { () => ShaderDefinition.Degrees(vec4), ToLower },

                { () => ShaderDefinition.Sin(_float), ToLower },
                { () => ShaderDefinition.Sin(vec2), ToLower },
                { () => ShaderDefinition.Sin(vec3), ToLower },
                { () => ShaderDefinition.Sin(vec4), ToLower },

                { () => ShaderDefinition.Cos(_float), ToLower },
                { () => ShaderDefinition.Cos(vec2), ToLower },
                { () => ShaderDefinition.Cos(vec3), ToLower },
                { () => ShaderDefinition.Cos(vec4), ToLower },

                { () => ShaderDefinition.Tan(_float), ToLower },
                { () => ShaderDefinition.Tan(vec2), ToLower },
                { () => ShaderDefinition.Tan(vec3), ToLower },
                { () => ShaderDefinition.Tan(vec4), ToLower },

                { () => ShaderDefinition.Asin(_float), ToLower },
                { () => ShaderDefinition.Asin(vec2), ToLower },
                { () => ShaderDefinition.Asin(vec3), ToLower },
                { () => ShaderDefinition.Asin(vec4), ToLower },

                { () => ShaderDefinition.Acos(_float), ToLower },
                { () => ShaderDefinition.Acos(vec2), ToLower },
                { () => ShaderDefinition.Acos(vec3), ToLower },
                { () => ShaderDefinition.Acos(vec4), ToLower },

                { () => ShaderDefinition.Atan2(_float, _float), ToLower },
                { () => ShaderDefinition.Atan2(vec2, vec2), ToLower },
                { () => ShaderDefinition.Atan2(vec3, vec3), ToLower },
                { () => ShaderDefinition.Atan2(vec4, vec4), ToLower },

                { () => ShaderDefinition.Atan(_float), ToLower },
                { () => ShaderDefinition.Atan(vec2), ToLower },
                { () => ShaderDefinition.Atan(vec3), ToLower },
                { () => ShaderDefinition.Atan(vec4), ToLower },

                { () => ShaderDefinition.Sinh(_float), ToLower },
                { () => ShaderDefinition.Sinh(vec2), ToLower },
                { () => ShaderDefinition.Sinh(vec3), ToLower },
                { () => ShaderDefinition.Sinh(vec4), ToLower },

                { () => ShaderDefinition.Cosh(_float), ToLower },
                { () => ShaderDefinition.Cosh(vec2), ToLower },
                { () => ShaderDefinition.Cosh(vec3), ToLower },
                { () => ShaderDefinition.Cosh(vec4), ToLower },

                { () => ShaderDefinition.Tanh(_float), ToLower },
                { () => ShaderDefinition.Tanh(vec2), ToLower },
                { () => ShaderDefinition.Tanh(vec3), ToLower },
                { () => ShaderDefinition.Tanh(vec4), ToLower },

                { () => ShaderDefinition.Asinh(_float), Redirect<Workarounds.Trigonometric>() },
                { () => ShaderDefinition.Asinh(vec2), Redirect<Workarounds.Trigonometric>() },
                { () => ShaderDefinition.Asinh(vec3), Redirect<Workarounds.Trigonometric>() },
                { () => ShaderDefinition.Asinh(vec4), Redirect<Workarounds.Trigonometric>() },

                { () => ShaderDefinition.Acosh(_float), Redirect<Workarounds.Trigonometric>() },
                { () => ShaderDefinition.Acosh(vec2), Redirect<Workarounds.Trigonometric>() },
                { () => ShaderDefinition.Acosh(vec3), Redirect<Workarounds.Trigonometric>() },
                { () => ShaderDefinition.Acosh(vec4), Redirect<Workarounds.Trigonometric>() },

                { () => ShaderDefinition.Atanh(_float), Redirect<Workarounds.Trigonometric>() },
                { () => ShaderDefinition.Atanh(vec2), Redirect<Workarounds.Trigonometric>() },
                { () => ShaderDefinition.Atanh(vec3), Redirect<Workarounds.Trigonometric>() },
                { () => ShaderDefinition.Atanh(vec4), Redirect<Workarounds.Trigonometric>() },

                { () => ShaderDefinition.SinCos(_float, out _float, out _float), ToLower },
                { () => ShaderDefinition.SinCos(vec2, out vec2, out vec2), ToLower },
                { () => ShaderDefinition.SinCos(vec3, out vec3, out vec3), ToLower },
                { () => ShaderDefinition.SinCos(vec4, out vec4, out vec4), ToLower },
                
                #endregion

                #region Exponential

                { () => ShaderDefinition.Pow(_float, _float), ToLower },
                { () => ShaderDefinition.Pow(vec2, vec2), ToLower },
                { () => ShaderDefinition.Pow(vec3, vec3), ToLower },
                { () => ShaderDefinition.Pow(vec4, vec4), ToLower },

                { () => ShaderDefinition.Exp(_float), ToLower },
                { () => ShaderDefinition.Exp(vec2), ToLower },
                { () => ShaderDefinition.Exp(vec3), ToLower },
                { () => ShaderDefinition.Exp(vec4), ToLower },

                { () => ShaderDefinition.Log(_float), ToLower },
                { () => ShaderDefinition.Log(vec2), ToLower },
                { () => ShaderDefinition.Log(vec3), ToLower },
                { () => ShaderDefinition.Log(vec4), ToLower },

                { () => ShaderDefinition.Exp2(_float), ToLower },
                { () => ShaderDefinition.Exp2(vec2), ToLower },
                { () => ShaderDefinition.Exp2(vec3), ToLower },
                { () => ShaderDefinition.Exp2(vec4), ToLower },

                { () => ShaderDefinition.Log2(_float), ToLower },
                { () => ShaderDefinition.Log2(vec2), ToLower },
                { () => ShaderDefinition.Log2(vec3), ToLower },
                { () => ShaderDefinition.Log2(vec4), ToLower },

                { () => ShaderDefinition.Sqrt(_float), ToLower },
                { () => ShaderDefinition.Sqrt(vec2), ToLower },
                { () => ShaderDefinition.Sqrt(vec3), ToLower },
                { () => ShaderDefinition.Sqrt(vec4), ToLower },

                { () => ShaderDefinition.InverseSqrt(_float), ToLower },
                { () => ShaderDefinition.InverseSqrt(vec2), ToLower },
                { () => ShaderDefinition.InverseSqrt(vec3), ToLower },
                { () => ShaderDefinition.InverseSqrt(vec4), ToLower },

                // HLSL does not define Sqrt / InverseSqrt on doubles and we should treat this 
                // as an error rather than emulate with precision loss,
                // because the double version normally gets explicitly requested

                { () => ShaderDefinition.Log10(_float), ToLower },
                { () => ShaderDefinition.Log10(vec2), ToLower },
                { () => ShaderDefinition.Log10(vec3), ToLower },
                { () => ShaderDefinition.Log10(vec4), ToLower },

                { () => ShaderDefinition.Exp10(_float), Redirect<Workarounds.Exponential>() },
                { () => ShaderDefinition.Exp10(vec2), Redirect<Workarounds.Exponential>() },
                { () => ShaderDefinition.Exp10(vec3), Redirect<Workarounds.Exponential>() },
                { () => ShaderDefinition.Exp10(vec4), Redirect<Workarounds.Exponential>() },

                #endregion

                #region Geometric

                { () => ShaderDefinition.Length(_float), ToLower },
                { () => ShaderDefinition.Length(vec2), ToLower },
                { () => ShaderDefinition.Length(vec3), ToLower },
                { () => ShaderDefinition.Length(vec4), ToLower },

                // No double support for HLSL

                { () => ShaderDefinition.Distance(_float, _float), ToLower },
                { () => ShaderDefinition.Distance(vec2, vec2), ToLower },
                { () => ShaderDefinition.Distance(vec3, vec3), ToLower },
                { () => ShaderDefinition.Distance(vec4, vec4), ToLower },

                // No double support for HLSL

                { () => ShaderDefinition.Dot(_float, _float), ToLower },
                { () => ShaderDefinition.Dot(vec2, vec2), ToLower },
                { () => ShaderDefinition.Dot(vec3, vec3), ToLower },
                { () => ShaderDefinition.Dot(vec4, vec4), ToLower },

                // No double support for HLSL

                { () => ShaderDefinition.Cross(vec3, vec3), ToLower },

                // No double support for HLSL

                { () => ShaderDefinition.Normalize(_float), ToLower },
                { () => ShaderDefinition.Normalize(vec2), ToLower },
                { () => ShaderDefinition.Normalize(vec3), ToLower },
                { () => ShaderDefinition.Normalize(vec4), ToLower },

                // No double support for HLSL

                { () => ShaderDefinition.FaceForward(_float, _float, _float), ToLower },
                { () => ShaderDefinition.FaceForward(vec2, vec2, vec2), ToLower },
                { () => ShaderDefinition.FaceForward(vec3, vec3, vec3), ToLower },
                { () => ShaderDefinition.FaceForward(vec4, vec4, vec4), ToLower },

                // No double support for HLSL

                { () => ShaderDefinition.Reflect(_float, _float), ToLower },
                { () => ShaderDefinition.Reflect(vec2, vec2), ToLower },
                { () => ShaderDefinition.Reflect(vec3, vec3), ToLower },
                { () => ShaderDefinition.Reflect(vec4, vec4), ToLower },

                // No double support for HLSL

                { () => ShaderDefinition.Refract(_float, _float, _float), Refract },
                { () => ShaderDefinition.Refract(vec2, vec2, _float), Refract },
                { () => ShaderDefinition.Refract(vec3, vec3, _float), Refract },
                { () => ShaderDefinition.Refract(vec4, vec4, _float), Refract },

                // No double support for HLSL

                #endregion

                #region Common

                { () => ShaderDefinition.Abs(_float), ToLower },
                { () => ShaderDefinition.Abs(vec2), ToLower },
                { () => ShaderDefinition.Abs(vec3), ToLower },
                { () => ShaderDefinition.Abs(vec4), ToLower },

                { () => ShaderDefinition.Abs(_int), ToLower },
                { () => ShaderDefinition.Abs(ivec2), ToLower },
                { () => ShaderDefinition.Abs(ivec3), ToLower },
                { () => ShaderDefinition.Abs(ivec4), ToLower },

                // No double support for HLSL

                { () => ShaderDefinition.Floor(_float), ToLower },
                { () => ShaderDefinition.Floor(vec2), ToLower },
                { () => ShaderDefinition.Floor(vec3), ToLower },
                { () => ShaderDefinition.Floor(vec4), ToLower },

                // No double support for HLSL

                { () => ShaderDefinition.Trunc(_float), ToLower },
                { () => ShaderDefinition.Trunc(vec2), ToLower },
                { () => ShaderDefinition.Trunc(vec3), ToLower },
                { () => ShaderDefinition.Trunc(vec4), ToLower },

                // No double support for HLSL

                { () => ShaderDefinition.Round(_float), ToLower },
                { () => ShaderDefinition.Round(vec2), ToLower },
                { () => ShaderDefinition.Round(vec3), ToLower },
                { () => ShaderDefinition.Round(vec4), ToLower },

                // No double support for HLSL

                // genType RoundEven(genType x) unsupported atm
                // genDType RoundEven(genDType x) unsupport

                { () => ShaderDefinition.Ceiling(_float), Rename("ceil") },
                { () => ShaderDefinition.Ceiling(vec2), Rename("ceil") },
                { () => ShaderDefinition.Ceiling(vec3), Rename("ceil") },
                { () => ShaderDefinition.Ceiling(vec4), Rename("ceil") },

                // No double support for HLSL

                { () => ShaderDefinition.Fraction(_float), Rename("frac") },
                { () => ShaderDefinition.Fraction(vec2), Rename("frac") },
                { () => ShaderDefinition.Fraction(vec3), Rename("frac") },
                { () => ShaderDefinition.Fraction(vec4), Rename("frac") },

                // No double support for HLSL

                { () => ShaderDefinition.Min(_float, _float), ToLower },
                // TODO: these overloads need WidenType
                // { () => ShaderDefinition.Min(vec2, _float), ToLower },
                // { () => ShaderDefinition.Min(vec3, _float), ToLower },
                // { () => ShaderDefinition.Min(vec4, _float), ToLower },

                { () => ShaderDefinition.Min(vec2, vec2), ToLower },
                { () => ShaderDefinition.Min(vec3, vec3), ToLower },
                { () => ShaderDefinition.Min(vec4, vec4), ToLower },

                // No double support for HLSL

                { () => ShaderDefinition.Min(_int, _int), ToLower },
                // TODO: these overloads need WidenType
                //{ () => ShaderDefinition.Min(ivec2, _int), ToLower },
                //{ () => ShaderDefinition.Min(ivec3, _int), ToLower },
                //{ () => ShaderDefinition.Min(ivec4, _int), ToLower },

                { () => ShaderDefinition.Min(ivec2, ivec2), ToLower },
                { () => ShaderDefinition.Min(ivec3, ivec3), ToLower },
                { () => ShaderDefinition.Min(ivec4, ivec4), ToLower },

                // No uint support for HLSL

                { () => ShaderDefinition.Max(_float, _float), ToLower },
                // TODO: these overloads need WidenType
                // { () => ShaderDefinition.Max(vec2, _float), ToLower },
                // { () => ShaderDefinition.Max(vec3, _float), ToLower },
                // { () => ShaderDefinition.Max(vec4, _float), ToLower },

                { () => ShaderDefinition.Max(vec2, vec2), ToLower },
                { () => ShaderDefinition.Max(vec3, vec3), ToLower },
                { () => ShaderDefinition.Max(vec4, vec4), ToLower },

                // No double support for HLSL

                { () => ShaderDefinition.Max(_int, _int), ToLower },
                // TODO: these overloads need WidenType
                //{ () => ShaderDefinition.Max(ivec2, _int), ToLower },
                //{ () => ShaderDefinition.Max(ivec3, _int), ToLower },
                //{ () => ShaderDefinition.Max(ivec4, _int), ToLower },

                { () => ShaderDefinition.Max(ivec2, ivec2), ToLower },
                { () => ShaderDefinition.Max(ivec3, ivec3), ToLower },
                { () => ShaderDefinition.Max(ivec4, ivec4), ToLower },

                // No uint support for HLSL

                { () => ShaderDefinition.Clamp(_float, _float, _float), ToLower },
                // TODO: these overloads need WidenType
                // { () => ShaderDefinition.Clamp(vec2, _float, _float), ToLower },
                // { () => ShaderDefinition.Clamp(vec3, _float, _float), ToLower },
                // { () => ShaderDefinition.Clamp(vec4, _float, _float), ToLower },

                { () => ShaderDefinition.Clamp(vec2, vec2, vec2), ToLower },
                { () => ShaderDefinition.Clamp(vec3, vec3, vec3), ToLower },
                { () => ShaderDefinition.Clamp(vec4, vec4, vec4), ToLower },

                // No double support for HLSL

                { () => ShaderDefinition.Clamp(_int, _int, _int), ToLower },
                // TODO: these overloads need WidenType
                //{ () => ShaderDefinition.Clamp(ivec2, _int, _int), ToLower },
                //{ () => ShaderDefinition.Clamp(ivec3, _int, _int), ToLower },
                //{ () => ShaderDefinition.Clamp(ivec4, _int, _int), ToLower },

                { () => ShaderDefinition.Clamp(ivec2, ivec2, ivec2), ToLower },
                { () => ShaderDefinition.Clamp(ivec3, ivec3, ivec3), ToLower },
                { () => ShaderDefinition.Clamp(ivec4, ivec4, ivec4), ToLower },

                // No uint support for HLSL

                { () => ShaderDefinition.Lerp(_float, _float, _float), ToLower },
                // TODO: these overloads need WidenType
                // { () => ShaderDefinition.Lerp(vec2, vec2, _float), ToLower },
                // { () => ShaderDefinition.Lerp(vec3, vec3, _float), ToLower },
                // { () => ShaderDefinition.Lerp(vec4, vec4, _float), ToLower },

                { () => ShaderDefinition.Lerp(vec2, vec2, vec2), ToLower },
                { () => ShaderDefinition.Lerp(vec3, vec3, vec3), ToLower },
                { () => ShaderDefinition.Lerp(vec4, vec4, vec4), ToLower },

                // No double support for HLSL

                // No bool support for HLSL

                { () => ShaderDefinition.Step(_float, _float), ToLower },
                // TODO: these overloads need WidenType
                // { () => ShaderDefinition.Step(_float, vec2), ToLower },
                // { () => ShaderDefinition.Step(_float, vec3), ToLower },
                // { () => ShaderDefinition.Step(_float, vec4), ToLower },

                { () => ShaderDefinition.Step(vec2, vec2), ToLower },
                { () => ShaderDefinition.Step(vec3, vec3), ToLower },
                { () => ShaderDefinition.Step(vec4, vec4), ToLower },

                // No double support for HLSL

                { () => ShaderDefinition.SmoothStep(_float, _float, _float), ToLower },
                // TODO: these overloads need WidenType
                // { () => ShaderDefinition.SmoothStep(_float, _float, vec2), ToLower },
                // { () => ShaderDefinition.SmoothStep(_float, _float, vec3), ToLower },
                // { () => ShaderDefinition.SmoothStep(_float, _float, vec4), ToLower },

                { () => ShaderDefinition.SmoothStep(vec2, vec2, vec2), ToLower },
                { () => ShaderDefinition.SmoothStep(vec3, vec3, vec3), ToLower },
                { () => ShaderDefinition.SmoothStep(vec4, vec4, vec4), ToLower },

                // No double support for HLSL

                #endregion

                { () => ShaderDefinition.texture(sampler2D, vec2), Rename("tex2D") },

                { () => ShaderDefinition.mod(vec2, _float), (m, i) => ModFloat<ShaderDefinition.vec2>(m, i) },
                { () => ShaderDefinition.mod(vec3, _float), (m, i) => ModFloat<ShaderDefinition.vec2>(m, i) },
                { () => ShaderDefinition.mod(vec4, _float), (m, i) => ModFloat<ShaderDefinition.vec2>(m, i) },
                { () => ShaderDefinition.mod(_float, _float), Rename("fmod") },
                { () => ShaderDefinition.mod(vec2, vec2), Rename("fmod") },
                { () => ShaderDefinition.mod(vec3, vec3), Rename("fmod") },
                { () => ShaderDefinition.mod(vec4, vec4), Rename("fmod") },

                { () => ShaderDefinition.dFdx(_float), Rename("ddx") },
                { () => ShaderDefinition.dFdx(vec2), Rename("ddx") },
                { () => ShaderDefinition.dFdx(vec3), Rename("ddx") },
                { () => ShaderDefinition.dFdx(vec4), Rename("ddx") },

                { () => ShaderDefinition.dFdy(_float), Rename("ddy") },
                { () => ShaderDefinition.dFdy(vec2), Rename("ddy") },
                { () => ShaderDefinition.dFdy(vec3), Rename("ddy") },
                { () => ShaderDefinition.dFdy(vec4), Rename("ddy") },

                { () => ShaderDefinition.textureGrad(sampler2D, vec2, vec2, vec2), Rename("tex2Dgrad") },
            };
        }

   
        private Expression WidenType<T>(Expression source)
        {        
            var tref = ShaderDefinition.ToCecil(typeof(T));
            var n = new ObjectCreateExpression( AstBuilder.ConvertType(tref), new[] { source.Clone() });            
            return n;
        }

        private StringBuilder ModFloat<T>(MethodDefinition m, InvocationExpression i)
        {
            // mod() will have 2 args we need to widen the rhs to a T1 however

            Debug.Assert(i.Arguments.Count == 2);
            var result = new StringBuilder();

            var lhs = i.Arguments.Take(1);
            var rhs = i.Arguments.Skip(1).Take(1);
            var widen = rhs.Select(WidenType<T>);
            var args = lhs.Concat(widen).ToList();

            result.Append("fmod(").Append(ArgsToString(args)).Append(")");

            return result;
        }

        private StringBuilder Refract(MethodDefinition m, InvocationExpression i)
        {
            Debug.Assert(i.Arguments.Count == 3);
            var result = new StringBuilder();

            var args = i.Arguments.ToArray();
            var a1 = args[1];
            args[1] = args[2];
            args[2] = a1;

            return result.Append("refract(").Append(ArgsToString(args)).Append(")");
        }
    }
}
