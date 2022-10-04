const stepDelay = 500;

describe('full spec', () => {
  it('passes', () => {
    cy.visit('http://127.0.0.1:9000/index.html')
    cy.contains('todos')

    // Wait to bind events
    cy.wait(1500);

    // Check we have 0 items
    cy.get('.todo-count').contains('0 items left');
    cy.get('.todo-list').find('li').should('have.length', 0);
    
    // Add new item
    cy.get('.new-todo').type('Buy milk').should('have.value', 'Buy milk').blur();
    cy.wait(stepDelay);
    
    // Check we have 1 not-completed item
    cy.get('.todo-list').find('li').should('have.length', 1);
    cy.get('.todo-count').contains('1 item left');
    cy.get('.todo-list').find('li').first().contains('Buy milk').should('not.have.class', 'completed');
    
    // Complete item
    cy.get('.todo-list').find('li').first().find('input[type=checkbox]').click();
    cy.wait(stepDelay);
    
    // Check we have 0 not-completed items
    cy.get('.todo-list').find('li').first().should('have.class', 'completed');
    cy.get('.todo-count').contains('0 items left');
    
    // Delete item
    cy.get('.todo-list').find('li').first().find('.destroy').click({ force: true });
    cy.wait(stepDelay);
    
    // Check we have 0 items
    cy.get('.todo-count').contains('0 items left');
    cy.get('.todo-list').find('li').should('have.length', 0);
  })
})