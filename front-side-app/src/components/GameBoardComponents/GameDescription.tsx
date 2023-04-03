import React from "react";

interface Props {
  categories: string[];
  types: string[];
  description: string;
  rules?: string;
}

const GameDescription: React.FC<Props> = ({
  categories,
  types,
  description,
  rules,
}) => {
  return (
    <div className="bg-gray-800 text-gray-200 p-4">
      <div className="mb-4">
        <h2 className="text-lg font-medium mb-2">Categories:</h2>
        <div className="flex flex-wrap gap-2">
          {categories.map((category) => (
            <span
              key={category}
              className="bg-gray-600 px-2 py-1 rounded-full text-sm"
            >
              {category}
            </span>
          ))}
        </div>
      </div>
      <div className="mb-4">
        <h2 className="text-lg font-medium mb-2">Types:</h2>
        <div className="flex flex-wrap gap-2">
          {types.map((type) => (
            <span
              key={type}
              className="bg-gray-600 px-2 py-1 rounded-full text-sm"
            >
              {type}
            </span>
          ))}
        </div>
      </div>
      <div className="mb-4">
        <h2 className="text-lg font-medium mb-2">Description:</h2>
        <div className="whitespace-pre-line break-words">{description}</div>
      </div>
      {rules && (
        <div className="mb-4">
          <h2 className="text-lg font-medium mb-2">Rules:</h2>
          <div className="whitespace-pre-line break-words">{rules}</div>
        </div>
      )}
    </div>
  );
};

export default GameDescription;
