import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { QuestionContent } from '../models/question-content';

@Injectable({
  providedIn: 'root'
})
export class ParaghraphService {

  constructor(private readonly client: HttpClient) { }

  public async getNextParagraph(): Promise<QuestionContent> {
    return await this.client.post<QuestionContent>(environment.baseUrl +'/api/Paraghraph/GetNextParaghraph', null).toPromise();
  }
}
