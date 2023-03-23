import React from "react";

interface DividerProps {
  label: string;
}

const SectionDivider: React.FC<DividerProps> = ({ label }) => {
  return (
    <div className="flex items-center justify-center my-6">
      <div className="border-t-2 border-green-500 w-1/3"></div>
      <div className="mx-3 text-lg text-gray-600 font-semibold text-center">
        {label}
      </div>
      <div className="border-t-2 border-green-500 w-1/3"></div>
    </div>
  );
};

export default SectionDivider;
