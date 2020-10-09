using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Pulsar.Contexts.OpenGL;

namespace Pulsar.Graphics
{
    public class GPUContext
    {
        private List<GPUBuffer> _buffers = new List<GPUBuffer>();
        private List<uint> _textures = new List<uint>();
        private List<uint> _framebuffers = new List<uint>();
        private List<uint> _shaders = new List<uint>();
        private List<uint> _programs = new List<uint>();
        private List<GPUPipeline> _pipelines = new List<GPUPipeline>();
        private List<uint> _vaos = new List<uint>();

        private GPUResource _currentFramebuffer = new GPUResource(0);

        private GPUResource _currentVao;

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

        public unsafe GPUShaderProgram CreateShaderProgram(GPUShader vertex, GPUShader fragment, GPUShader geometry = null, GPUShader tessEval = null, GPUShader tessCtrl = null)
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
            Gl.glGetProgramiv(sProgram, Gl.GL_LINK_STATUS, &success);
            if (success == 0)
            {
                StringBuilder builder = new StringBuilder(512);
                int length;
                Gl.glGetProgramInfoLog(sProgram, 512, out length, builder);
                Console.WriteLine(builder.ToString());
            }

            return sProgram;
        }

        public GPUPipelineFormat CreatePipelineFormat(GPUBufferFormat[] bufferFormats, GPUVertexFormat[] vertexFormats)
        {
            GPUPipelineFormat f = new GPUPipelineFormat(Gl.glCreateVertexArray(), bufferFormats, vertexFormats);
            BindFormat(f);
            for (uint i = 0; i < f.GetVertexFormats().Length; i++)
            {
                GPUVertexFormat vf = f.GetVertexFormats()[i];
                Gl.glEnableVertexArrayAttrib(f, vf.Location);
                Gl.glVertexArrayAttribFormat(f, vf.Location, (int)vf.Size, vf.Type, vf.Normalized, vf.RelativeOffset);
                Gl.glVertexArrayAttribBinding(f, vf.Location, vf.BufferIndex);
            }
            _vaos.Add(f);
            return f;
        }

        public void DeletePipelineFormat(GPUPipelineFormat format)
        {
            _vaos.Remove(format);
            Gl.glDeleteVertexArrays(1, new uint[]{format});
        }
        
        public GPUPipeline CreatePipeline(GPUPipelineCreateInfo info)
        {
            GPUBuffer[] buffers = new GPUBuffer[info.Format.GetBufferFormats().Length];
            int i;
            for (i = 0; i < info.Format.GetBufferFormats().Length; i++)
            {
                buffers[i] = CreateBuffer(info.Format.GetBufferFormats()[i].Size);
            }
            for (i = 0; i < info.Format.GetBufferFormats().Length; i++)
            {
                GPUBufferFormat bf = info.Format.GetBufferFormats()[i];
                Gl.glVertexArrayVertexBuffer(info.Format, bf.BufferIndex, _buffers[i], bf.Offset, (int)bf.Stride);
            }
            GPUPipeline pipe = new GPUPipeline(this, info.ShaderProgram, info.Format, buffers);
            _pipelines.Add(pipe);
            return pipe;
        }

        public void DeletePipeline(GPUPipeline pipe)
        {
            //TODO: delete all handles in pipeline
            _pipelines.Remove(pipe);
        }

        public GPUBuffer CreateBuffer(uint size)
        {
            GPUBuffer buffer = new GPUBuffer(Gl.glCreateBuffer(), size);
            Gl.glNamedBufferStorage(buffer, size, IntPtr.Zero, Gl.GL_DYNAMIC_STORAGE_BIT);
            _buffers.Add(buffer);
            return buffer;
        }

        public void DeleteBuffer(GPUBuffer buffer)
        {
            Gl.glDeleteBuffers(1, new uint[]{buffer});
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
            Gl.glDeleteTextures(1, new uint[]{texture});
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
            Gl.glDeleteFramebuffers(1, new uint[]{framebuffer});
            _framebuffers.Remove(framebuffer);
        }

        public GPUFramebuffer GetDefaultFramebuffer()
        {
            return new GPUFramebuffer(0);
        }

        public void BindFormat(GPUPipelineFormat format)
        {
            if (_currentVao != format)
            {
                Gl.glBindVertexArray(format);
                _currentVao = format;
            }
        }

        public void BindShaderProgram(GPUShaderProgram program)
        {
            if (_currentProgram != program)
            {
                Gl.glUseProgram(program);
                _currentProgram = program;
            }
        }

        public void BindFramebuffer(GPUFramebuffer fb)
        {
            if (_currentFramebuffer != fb)
            {
                Gl.glBindFramebuffer(Gl.GL_FRAMEBUFFER, fb);
                _currentFramebuffer = fb;
            }
        }

        public void ClearFramebuffer(GPUFramebuffer fb, BufferMasks mask)
        {
            BindFramebuffer(fb);
            Gl.glClear((uint)mask);
        }
        
        public unsafe void ReadLogs()
        {
            uint nMsg = 100;
            int maxMsgLen = 0;
            Gl.glGetIntegerv(Gl.GL_MAX_DEBUG_MESSAGE_LENGTH, &maxMsgLen);
            StringBuilder b = new StringBuilder();
            int[] sources = new int[nMsg];
            int[] types = new int[nMsg];
            int[] severities = new int[nMsg];
            uint[] ids = new uint[nMsg];
            int[] lengths = new int[nMsg];

            uint count = Gl.glGetDebugMessageLog(nMsg, maxMsgLen, sources, types, ids, severities, lengths, b);
            Console.WriteLine(b);
        }

        public void ReadErrors()
        {
            int err;
            while ((err = Gl.glGetError()) != Gl.GL_NO_ERROR)
            {
                string msg = "";
                switch (err)
                {
                    case Gl.GL_INVALID_ENUM:                  msg = "INVALID_ENUM"; break;
                    case Gl.GL_INVALID_VALUE:                 msg = "INVALID_VALUE"; break;
                    case Gl.GL_INVALID_OPERATION:             msg = "INVALID_OPERATION"; break;
                    case Gl.GL_STACK_OVERFLOW:                msg = "STACK_OVERFLOW"; break;
                    case Gl.GL_STACK_UNDERFLOW:               msg = "STACK_UNDERFLOW"; break;
                    case Gl.GL_OUT_OF_MEMORY:                 msg = "OUT_OF_MEMORY"; break;
                    case Gl.GL_INVALID_FRAMEBUFFER_OPERATION: msg = "INVALID_FRAMEBUFFER_OPERATION"; break;
                }
                Console.WriteLine("OpenGL error : " + msg);
            }
        }
    }
}