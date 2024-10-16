<template>
  <div class="intro-screen">
    <video
      ref="introVideo"
      autoplay
      controls
      @canplay="playVideo"
      class="small-video"
    >
      <source src="/assets/IntroVideo.mp4" type="video/mp4" />
      Tu navegador no soporta la reproducción de videos.
    </video>
  </div>
</template>

<script>
export default {
  name: 'IntroScreen',
  methods: {
    playVideo() {
      const video = this.$refs.introVideo;
      video.play().catch((error) => {
        console.log("Reproducción automática bloqueada:", error);
      });
    },
  },
  mounted() {
    setTimeout(() => {
      this.$router.push('/main');
    }, 6000);
  },
};
</script>

<style scoped>
.intro-screen {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100vh;
  background-color: #9A2B2B;
}

.small-video {
  width: 60vw;
  height: auto;
  max-width: 80vw;
  border-radius: 10px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.3);
}
</style>