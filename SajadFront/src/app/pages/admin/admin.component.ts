import { Component, OnInit } from '@angular/core';
import { ContentService } from 'src/app/services/content.service';
import { DocumentService } from 'src/app/services/document.service';
import { ParaghraphService } from 'src/app/services/paraghraph.service';
import { QuestionService } from 'src/app/services/question.service';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})
export class AdminComponent implements OnInit {

  constructor(
    private readonly service: ContentService,
    private readonly documentService: DocumentService,
    private readonly paraghraphService : ParaghraphService,
    private readonly questionService: QuestionService) { }

  public fileToUpload?: File = undefined;

  public documentCount = 0;
  public paraghraphCount = 0;
  public questionCount = 0;

  ngOnInit(): void {
    this.documentService.getCount().then(count => this.documentCount = count);
    this.paraghraphService.getCount().then(count => this.paraghraphCount = count);
    this.questionService.getCount().then(count => this.questionCount = count);
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
