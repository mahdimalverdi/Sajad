import { Component, OnInit } from '@angular/core';
import { ContentService } from 'src/app/services/content.service';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})
export class AdminComponent implements OnInit {

  constructor(private readonly service: ContentService) { }

  public fileToUpload?: File = undefined;

  ngOnInit(): void {
  }

  public async handleFileInput(event: any) {
    this.fileToUpload = event?.target?.files?.item(0) ?? undefined;
    await this.uploadFileToActivity();
  }

  public async uploadFileToActivity() {
    if (this.fileToUpload) {
      this.service.uploadFile(this.fileToUpload);
    }
  }

}
