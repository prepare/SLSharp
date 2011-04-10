using System;

namespace IIS.SLSharp.Reflection
{
    [Serializable]
    internal enum ReflectionToken
    {
        ShaderName,
        ShaderActivate,
        ShaderVec2Helper,
        ShaderVec3Helper,
        ShaderVec4Helper,
        ShaderUniformMatrix2X2Helper,
        ShaderUniformMatrix2X3Helper,
        ShaderUniformMatrix2X4Helper,
        ShaderUniformMatrix3X2Helper,
        ShaderUniformMatrix3X3Helper,
        ShaderUniformMatrix3X4Helper,
        ShaderUniformMatrix4X2Helper,
        ShaderUniformMatrix4X3Helper,
        ShaderUniformMatrix4X4Helper,
        ShaderBegin,
        ResourceHelperRelease,
        ShaderSamplerHelper,
        ShaderIvec2Helper,
        ShaderIvec3Helper,
        ShaderIvec4Helper,
        ShaderUvec2Helper,
        ShaderUvec3Helper,
        ShaderUvec4Helper,
        ShaderDvec2Helper,
        ShaderDvec3Helper,
        ShaderDvec4Helper,
        ShaderUniformDMatrix2X2Helper,
        ShaderUniformDMatrix2X3Helper,
        ShaderUniformDMatrix2X4Helper,
        ShaderUniformDMatrix3X2Helper,
        ShaderUniformDMatrix3X3Helper,
        ShaderUniformDMatrix3X4Helper,
        ShaderUniformDMatrix4X2Helper,
        ShaderUniformDMatrix4X3Helper,
        ShaderUniformDMatrix4X4Helper,
        ShaderBvec1Helper,
        ShaderBvec2Helper,
        ShaderBvec3Helper,
        ShaderBvec4Helper,
    }
}
