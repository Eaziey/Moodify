<template>

<!-- Modal -->
<div class="modal fade" id="spotifyModal" tabindex="-1" aria-hidden="true" ref="spotifyModal">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="spotifyModalTitle">Sign In with Spotify</h5>
        <button type="button" class="btn-close" @click="closeModal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
        <h2>Would you like to Integrate with Spotify?</h2>
        <p>When you integrate with spotify you get your playlists and songs from spotify</p>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-success" @click="integrateWithSpotify(true)">Yes</button>
        <button type="button" class="btn btn-danger" @click="integrateWithSpotify(false)">No</button>
      </div>
    </div>
  </div>
</div>

</template>

<script>
    import { Modal } from 'bootstrap/dist/js/bootstrap.bundle.min';

    export default{
        name: "SpotifySignInModal",
        methods: {
          openModal(){
            const modal = new Modal(this.$refs.spotifyModal);
            modal.show()
          }, 
          closeModal(){
            const modal = Modal.getInstance(this.$refs.spotifyModal);
            modal.hide();
          },
          integrateWithSpotify(integrate){
            console.log("Integrate called: " ,integrate)
            this.$emit("integrate", integrate);
            this.closeModal();
          }
        }
        ,

    }
</script>