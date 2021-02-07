import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { PorsemanAnswer } from '../models/porseman-answer';
import { PorsemanQuestion } from '../models/porseman-question';

@Injectable({
  providedIn: 'root'
})
export class PorsemanService {

  constructor(private readonly client: HttpClient) { }

  public async ask(question: PorsemanQuestion): Promise<PorsemanAnswer> {
    return await this.client.post<PorsemanAnswer>('http://94.184.90.117:2110/qa', question).toPromise();
  }
}
