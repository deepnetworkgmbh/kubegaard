import { Component, Vue, Prop } from "vue-property-decorator";
import { DataService } from "@/services/DataService";
import Spinner from "@/components/spinner/Spinner.vue";
import { ImageScanDetailModel } from '@/models';

@Component({
  components: { Spinner }
})
export default class ImageDetail extends Vue {
  @Prop()
  imageid!: string;

  @Prop({ default: null })
  date!: string;

  loaded: boolean = false;
  service: DataService = new DataService();
  data: ImageScanDetailModel = new ImageScanDetailModel();

  created() {
    //this.id = decodeURIComponent(this.$route.params.imageid);
    const dateString = new Date(this.date).toDateString();
    console.log(`getting data for image ${this.imageid} with date ${dateString}`);
    this.service.getImageScanResultData(this.imageid, dateString)
      .then(response => {
        if (response) {
          this.data = response;
          this.loaded = true;
          this.setupPage();
        }
      });
  }

  setupPage() { }
}
