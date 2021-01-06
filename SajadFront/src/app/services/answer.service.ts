import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AnswerStruct } from '../models/answer-struct';

@Injectable({
  providedIn: 'root'
})
export class AnswerService {

  constructor(private readonly client: HttpClient) { }

  public async GetAnswersPerUser(userId: string): Promise<AnswerStruct[]> {
    return await this.client.post<AnswerStruct[]>(`/api/Answer/GetAnswersPerUser/${userId}`, null).toPromise();
  }
}
