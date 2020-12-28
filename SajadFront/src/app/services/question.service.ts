import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { AddQuestion } from '../models/add-question';

@Injectable({
  providedIn: 'root'
})
export class QuestionService {

  constructor(private readonly client: HttpClient) { }

  public async Add(question: AddQuestion): Promise<any> {
    return await this.client.post(environment.baseUrl + '/api/Questions/Add', question).toPromise();
  }

  public async getCount(): Promise<any> {
    return await this.client.post(environment.baseUrl + '/api/Questions/GetCount', null).toPromise();
  }
}
