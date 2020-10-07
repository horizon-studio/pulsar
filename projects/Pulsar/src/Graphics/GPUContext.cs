using System;
using System.Collections.Generic;
using System.Text;
using Pulsar.Contexts.OpenGL;

namespace Pulsar.Graphics
{
    public class GPUContext
    {
        private List<uint> _buffers = new List<uint>();
        private List<uint> _textures = new List<uint>();
        private List<uint> _framebuffers = new List<uint>();
        private List<uint> _shaders = new List<uint>();
        private List<uint> _programs = new List<uint>();
        private List<GPUPipeline> _pipelines = new List<GPUPipeline>();

        private GPUResource _currentFramebuffer = new GPUResource(0);

        private GPUResource _currentVAO;

        private GPUResource _currentProgram;
        

        public GPUShader CreateShader(string code, ShaderStages stage)
        {
            uint shader = Gl.glCreateShader((int) stage);
            _shaders.Add(shader);
            Gl.glShaderSource(shader, 1, new []{code}, new []{code.Length});
            Gl.glCompileShader(shader);
            int success = 1;
            Gl.glGetShaderiv(shader, Gl.GL_COMPILE_STATUS, new []{success});
            if (success == 0)
            {
                StringBuilder builder = new StringBuilder(512);
                int length;
                Gl.glGetShaderInfoLog(shader, 512, out length, builder);
                Console.WriteLine(builder.ToString());
            }
            return new GPUShader(shader, stage);
        }

        public void DeleteShader(GPUShader shader)
        {
            Gl.glDeleteShader(shader);
            _textures.Remove(shader);
        }

        public GPUShaderProgram CreateShaderProgram(GPUShader vertex, GPUShader fragment, GPUShader geometry, GPUShader tessEval, GPUShader tessCtrl)
        {
            GPUShaderProgram sProgram = new GPUShaderProgram(Gl.glCreateProgram());
            
            if (vertex != null)
            {
                Gl.glAttachShader(sProgram, vertex);
            }
            
            if (fragment != null)
            {
                Gl.glAttachShader(sProgram, fragment);
            }
            
            if (geometry != null)
            {
                Gl.glAttachShader(sProgram, geometry);
            }
            
            if (tessEval != null)
            {
                Gl.glAttachShader(sProgram, tessEval);
            }
            
            if (tessCtrl != null)
            {
                Gl.glAttachShader(sProgram, tessCtrl);
            }
            
            Gl.glLinkProgram(sProgram);
            int success = 1;
            Gl.glGetProgramiv(sProgram.GetHandle(), Gl.GL_LINK_STATUS, new []{success});
            if (success == 0)
            {
                StringBuilder builder = new StringBuilder(512);
                int length;
                Gl.glGetProgramInfoLog(sProgram.GetHandle(), 512, out length, builder);
                Console.WriteLine(builder.ToString());
            }

            return sProgram;
        }

        public GPUPipeline CreatePipeline(GPUPipelineCreateInfo info)
        {
            GPUShaderProgram sProgram =
                CreateShaderProgram(info.VertexShader, info.FragmentShader, info.GeometryShader, null, null);
            
            GPUResource vao = new GPUResource(Gl.glCreateVertexArray());
            GPUBuffer[] buffers = new GPUBuffer[info.Format.BufferFormats.Length];
            uint i;
            for (i = 0; i < info.Format.BufferFormats.Length; i++)
            {
                buffers[i] = new GPUBuffer(Gl.glCreateBuffer());
                Gl.glNamedBufferStorage(buffers[i].GetHandle(), info.Format.BufferFormats[i].Size, IntPtr.Zero, Gl.GL_DYNAMIC_STORAGE_BIT);
            }
            
            for (i = 0; i < info.Format.BufferFormats.Length; i++)
            {
                Gl.glVertexArrayVertexBuffer(vao.GetHandle(), info.Format.BufferFormats[i].BufferIndex, buffers[i].GetHandle(), info.Format.BufferFormats[i].Offset, (int)info.Format.BufferFormats[i].Stride);
                i++;
            }
            
            for (i = 0; i < info.Format.VertexFormats.Length; i++)
            {
                Gl.glEnableVertexArrayAttrib(vao.GetHandle(), i);
                Gl.glVertexArrayAttribFormat(vao.GetHandle(), i, (int)info.Format.VertexFormats[i].Size, info.Format.VertexFormats[i].Type, info.Format.VertexFormats[i].Normalized, info.Format.VertexFormats[i].RelativeOffset);
                Gl.glVertexArrayAttribBinding(vao.GetHandle(), i, info.Format.VertexFormats[i].BufferIndex);
                i++;
            }

            GPUBuffer indicesBuffer = null;
            if (info.Format.UseIndices)
            {
                indicesBuffer = new GPUBuffer(Gl.glCreateBuffer());
                Gl.glNamedBufferStorage(indicesBuffer.GetHandle(), info.Format.IndicesSize, IntPtr.Zero, info.Format.IndicesFlags);
            }
            
            GPUPipeline pipe = new GPUPipeline(this, sProgram, vao, buffers, null);
            _pipelines.Add(pipe);
            return pipe;
        }

        public void DeletePipeline(GPUPipeline pipe)
        {
            //TODO: delete all handles in pipeline
            _pipelines.Remove(pipe);
        }

        public GPUBuffer CreateBuffer()
        {
            GPUBuffer buffer = new GPUBuffer(Gl.glCreateBuffer());
            _buffers.Add(buffer.GetHandle());
            return buffer;
        }

        public void DeleteBuffer(GPUBuffer buffer)
        {
            Gl.glDeleteBuffers(0, new uint[]{buffer});
            _buffers.Remove(buffer);
        }

        public GPUTexture CreateTexture()
        {
            GPUTexture texture = new GPUTexture(Gl.glGenTexture());
            _textures.Add(texture);
            return texture;
        }

        public void DeleteTexture(GPUTexture texture)
        {
            Gl.glDeleteTextures(0, new uint[]{texture});
            _textures.Remove(texture);
        }

        public GPUFramebuffer CreateFramebuffer()
        {
            GPUFramebuffer framebuffer = new GPUFramebuffer(Gl.glCreateFramebuffer());
            _framebuffers.Add(framebuffer);
            return framebuffer;
        }

        public void DeleteFramebuffer(GPUFramebuffer framebuffer)
        {
            Gl.glDeleteFramebuffers(0, new uint[]{framebuffer});
            _framebuffers.Remove(framebuffer);
        }

        public GPUFramebuffer GetDefaultFramebuffer()
        {
            return new GPUFramebuffer(0);
        }
    }
}