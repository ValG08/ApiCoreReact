const { generateText, checkAndGenerate } = require('./util');
import assert from 'assert';
var request = require("request");

test('should output note message', () => {
    var textSend = 'message send';

    const text = generateText(textSend);
    expect(text).toBe(textSend);

    assert.notEqual(text, null)
    assert.equal(text.length, textSend.length);
})

test('should generate a valid text output', () => {
    var textSend = 'message send';

    const text = checkAndGenerate(textSend);
    expect(text).toBe(textSend);

    assert.notEqual(text, null)
})

// show call in cmd
 it('Api call get method', () => {
    var re = request.get({url: "https://localhost:5001/notes", "rejectUnauthorized": false});
   
})  


it('Api call get(id) method', () => {

    var id = 1;
    var re = request.get({url: "https://localhost:5001/notes/" + id, "rejectUnauthorized": false});
   
}) 