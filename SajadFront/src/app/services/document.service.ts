import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DocumentService {

  constructor(private readonly client: HttpClient) { }

  public async getCount(): Promise<any> {
    return await this.client.post(environment.baseUrl + '/api/Document/GetCount', null).toPromise();
  }
}
