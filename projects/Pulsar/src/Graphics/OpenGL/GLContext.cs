﻿using System;
using System.Collections.Generic;
using System.Text;
 using Pulsar.Graphics;
 using Pulsar.Graphics.OpenGL;

 namespace Ubersharp.Client.Rendering.OpenGL
{
    public class GLContext : GPUContext
    {
        private List<uint> _buffers = new List<uint>();
        private List<uint> _textures = new List<uint>();
        private List<uint> _framebuffers = new List<uint>();
        private List<uint> _shaders = new List<uint>();
        private List<GPUPipeline> _pipelines = new List<GPUPipeline>();
        //TODO: store current binded objects 
        
        public void Initialize()
        {
            
        }

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
            return new GLShader(shader, stage);
        }

        public void DeleteShader(GPUShader shader)
        {
            Gl.glDeleteShader(((GLShader)shader).GetHandle());
            _textures.Remove(((GLShader)shader).GetHandle());
        }

        public GPUPipeline CreatePipeline(GPUPipelineCreateInfo info)
        {
            GPUResource<uint> sProgram = new GPUResource<uint>(Gl.glCreateProgram());
            if (info.VertexShader != null)
            {
                Gl.glAttachShader(sProgram.GetHandle(), ((GLShader)info.VertexShader).GetHandle());
            }
            
            if (info.FragmentShader != null)
            {
                Gl.glAttachShader(sProgram.GetHandle(), ((GLShader)info.FragmentShader).GetHandle());
            }
            //TODO: other shaders
            Gl.glLinkProgram(sProgram.GetHandle());
            int success = 1;
            Gl.glGetProgramiv(sProgram.GetHandle(), Gl.GL_LINK_STATUS, new []{success});
            if (success == 0)
            {
                StringBuilder builder = new StringBuilder(512);
                int length;
                Gl.glGetProgramInfoLog(sProgram.GetHandle(), 512, out length, builder);
                Console.WriteLine(builder.ToString());
            }
            
            GPUResource<uint> vao = new GPUResource<uint>(Gl.glCreateVertexArray());
            GPUResource<uint>[] buffers = new GPUResource<uint>[info.Format.BufferFormats.Length];
            uint i;
            for (i = 0; i < info.Format.BufferFormats.Length; i++)
            {
                buffers[i] = new GPUResource<uint>(Gl.glCreateBuffer());
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

            GPUResource<uint> indicesBuffer = null;
            if (info.Format.UseIndices)
            {
                indicesBuffer = new GPUResource<uint>(Gl.glCreateBuffer());
                Gl.glNamedBufferStorage(indicesBuffer.GetHandle(), info.Format.IndicesSize, IntPtr.Zero, info.Format.IndicesFlags);
            }
            
            GPUPipeline pipe = new GLPipeline(this, sProgram, vao, buffers, null);
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
            GLBuffer buffer = new GLBuffer(Gl.glCreateBuffer());
            _buffers.Add(buffer.GetHandle());
            return buffer;
        }

        public void DeleteBuffer(GPUBuffer buffer)
        {
            Gl.glDeleteBuffers(0, new []{((GLBuffer)buffer).GetHandle()});
            _buffers.Remove(((GLBuffer)buffer).GetHandle());
        }

        public GPUTexture CreateTexture()
        {
            GLTexture texture = new GLTexture(Gl.glGenTexture());
            _textures.Add(texture.GetHandle());
            return texture;
        }

        public void DeleteTexture(GPUTexture texture)
        {
            Gl.glDeleteTextures(0, new []{((GLTexture)texture).GetHandle()});
            _textures.Remove(((GLTexture)texture).GetHandle());
        }

        public GPUFramebuffer CreateFramebuffer()
        {
            GLFramebuffer framebuffer = new GLFramebuffer(Gl.glCreateFramebuffer());
            _framebuffers.Add(framebuffer.GetHandle());
            return framebuffer;
        }

        public void DeleteFramebuffer(GPUFramebuffer framebuffer)
        {
            Gl.glDeleteFramebuffers(0, new []{((GLFramebuffer)framebuffer).GetHandle()});
            _framebuffers.Remove(((GLFramebuffer) framebuffer).GetHandle());
        }

        public GPUFramebuffer GetDefaultFramebuffer()
        {
            return new GLFramebuffer(0);
        }
    }
}