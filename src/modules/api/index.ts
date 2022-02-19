declare global {
  var db: any;
}

if (!global.db) {
  const sqlite3 = require('sqlite3').verbose();
  global.db = new sqlite3.Database(':memory:');
  var db = global.db;
}

import employees from './employees/employees';

initialize();

function query(pathStr: string): string {
  return require('fs')
    .readFileSync(pathStr, 'utf8');
}

function initialize(): void {
  createTables();
}

function createTables(): void {
  db.serialize(function() {
    createEmployeesTable();
  });
}

function createEmployeesTable(): void {
  db.run(query('/home/zbeach/workspace/projects/sim-production/src/util/queries/create_table.sql'), [], (err: Error) => {
    if (err) {
      throw err;
    }

    console.log('Created employees table');
  });
}

export {
  employees
};
