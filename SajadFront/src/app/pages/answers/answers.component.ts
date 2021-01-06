import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AnswerStruct } from 'src/app/models/answer-struct';
import { AnswerService } from 'src/app/services/answer.service';

@Component({
  selector: 'app-answers',
  templateUrl: './answers.component.html',
  styleUrls: ['./answers.component.scss']
})
export class AnswersComponent implements OnInit {

  public answers: AnswerStruct[] = [];
  public state: string = 'loading';

  constructor(
    private readonly answerService: AnswerService,
    private route: ActivatedRoute) { }

  async ngOnInit(): Promise<void> {
    const userId = this.route.snapshot.params.userId;
    this.answers = await this.answerService.GetAnswersPerUser(userId);
    this.state = 'ready';
  }

}
