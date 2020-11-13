import { TacoChallengeTemplatePage } from './app.po';

describe('TacoChallenge App', function() {
  let page: TacoChallengeTemplatePage;

  beforeEach(() => {
    page = new TacoChallengeTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
