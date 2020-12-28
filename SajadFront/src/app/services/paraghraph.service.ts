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
    return await this.client.post<QuestionContent>('/api/Paraghraph/GetNextParaghraph', null).toPromise();
  }

  public async getCount(): Promise<any> {
    return await this.client.post( '/api/Paraghraph/GetCount', null).toPromise();
  }
}
