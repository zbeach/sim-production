const { db } = global;

function query(pathStr: string) {
  return require('fs')
    .readFileSync(pathStr, 'utf8');
}

function post(employees: Employee | Employee[]): void {
  
}

export function get(ids: number | number[] | null) {
  // null or number[]
  if (typeof ids === 'object') {
    db.all(
      query('/home/zbeach/workspace/projects/sim-production/src/modules/api/employees/get_all.sql'),
      !ids ? [] : ids,
      (err: Error, rows: any[]) => {
        if (err) {
          return console.error(err.message);
        }
        return rows;
      }
    );
  }

  else if (typeof ids === 'number') {
    const id = ids;

    db.get(
      query('/home/zbeach/workspace/projects/sim-production/src/modules/api/employees/get_all.sql'),
      [ id ],
      (err: Error, row: any) => {
        if (err) {
          return console.error(err.message);
        }
        return row;
      }
    );
  }

  else throw new Error('Invalid argument');
};

export default { get };
