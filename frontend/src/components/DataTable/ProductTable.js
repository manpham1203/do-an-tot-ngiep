import React from "react";

function ProductTable(props) {
  return (
    <div className="overflow-hidden overflow-x-auto border border-gray-100 rounded">
      <table className="min-w-full text-sm divide-y divide-gray-200">
        <thead>
          <tr className="bg-gray-50">
            <th className="sticky left-0 px-4 py-2 text-left bg-gray-50">
              <label className="sr-only" htmlFor="row_all">
                Select All
              </label>
              <input
                className="w-5 h-5 border-gray-200 rounded"
                type="checkbox"
                id="row_all"
              />
            </th>
            <th className="px-4 py-2 font-medium text-left text-gray-900 whitespace-nowrap">
              Name
            </th>
            <th className="px-4 py-2 font-medium text-left text-gray-900 whitespace-nowrap">
              Date of Birth
            </th>
            <th className="px-4 py-2 font-medium text-left text-gray-900 whitespace-nowrap">
              Role
            </th>
            <th className="px-4 py-2 font-medium text-left text-gray-900 whitespace-nowrap">
              Salary
            </th>
          </tr>
        </thead>

        <tbody className="divide-y divide-gray-100">
          <tr>
            <td className="sticky left-0 px-4 py-2 bg-white">
              <label className="sr-only" htmlFor="row_1">
                Row 1
              </label>
              <input
                className="w-5 h-5 border-gray-200 rounded"
                type="checkbox"
                id="row_1"
              />
            </td>
            <td className="px-4 py-2 font-medium text-gray-900 whitespace-nowrap">
              John Doe
            </td>
            <td className="px-4 py-2 text-gray-700 whitespace-nowrap">
              24/05/1995
            </td>
            <td className="px-4 py-2 text-gray-700 whitespace-nowrap">
              Web Developer
            </td>
            <td className="px-4 py-2 text-gray-700 whitespace-nowrap">
              $120,000
            </td>
          </tr>

          <tr>
            <td className="sticky left-0 px-4 py-2 bg-white">
              <label className="sr-only" htmlFor="row_2">
                Row 2
              </label>
              <input
                className="w-5 h-5 border-gray-200 rounded"
                type="checkbox"
                id="row_2"
              />
            </td>
            <td className="px-4 py-2 font-medium whitespace-nowrap">
              Jane Doe
            </td>
            <td className="px-4 py-2 text-gray-700 whitespace-nowrap">
              04/11/1980
            </td>
            <td className="px-4 py-2 text-gray-700 whitespace-nowrap">
              Web Designer
            </td>
            <td className="px-4 py-2 text-gray-700 whitespace-nowrap">
              $100,000
            </td>
          </tr>

          <tr>
            <td className="sticky left-0 px-4 py-2 bg-white">
              <label className="sr-only" htmlFor="row_3">
                Row 3
              </label>
              <input
                className="w-5 h-5 border-gray-200 rounded"
                type="checkbox"
                id="row_3"
              />
            </td>
            <td className="px-4 py-2 font-medium whitespace-nowrap">
              Gary Barlow
            </td>
            <td className="px-4 py-2 text-gray-700 whitespace-nowrap">
              24/05/1995
            </td>
            <td className="px-4 py-2 text-gray-700 whitespace-nowrap">
              Singer
            </td>
            <td className="px-4 py-2 text-gray-700 whitespace-nowrap">
              $20,000
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  );
}

export default ProductTable;
