import { useState } from "react";
import styles from "./Experiences.module.css";
import { CxOption, CxSelect } from "@computas/designsystem/select/react";
import { experienceTypeMap } from "../types/experienceTypes";
import { useExperiences } from "../hooks/useExperiences";
import { ExperienceCard } from "../components/experiences/ExperienceCard";

export default function Experiences() {
  // eslint-disable-next-line @typescript-eslint/no-unused-vars
  const [_selectedExperience, _setSelectedExperience] = useState<string | null>(
    null
  );

  const { data: experiences, isLoading: isExperiencesLoading, error: experiencesError} = useExperiences();


  if (!experiences || experiences.length === 0) {
    return <div className={styles.noExperiences}>No experiences found.</div>;
  }

  const handleSelectChange = (e: Event) => {
    const customEvent = e as CustomEvent;
    const selectedFilter = customEvent.detail.value;
    console.log(selectedFilter);
    _setSelectedExperience(selectedFilter);
  };

  const filteredExperiences = () => {
    const validTypes = Object.keys(experienceTypeMap).filter(
      (type) => type !== "other"
    );

    if (_selectedExperience === "other") {
      return experiences.filter(
        (experience) => !validTypes.includes(experience.type.toLowerCase())
      );
    } else if (_selectedExperience) {
      return experiences.filter(
        (experience) =>
          experience.type.toLowerCase() === _selectedExperience.toLowerCase()
      );
    }
    return experiences;
  };

  if(isExperiencesLoading) {
    return (
      <div className={styles.loading}>Loading...</div>
    )
  }
  else if (experiencesError) {
    return (
      <div className={styles.container}>
          <div className={styles.loading}>Error...</div>
        </div>
    )
  } else {
    return (
      <div className={styles.container}>
        <div className={styles.select}>
          <label className="cx-form-field">
            <div className="cx-form-field__input-container">
              <CxSelect onChange={handleSelectChange}>
                <CxOption value="">Ingen filtrering</CxOption>
                {Object.entries(experienceTypeMap).map(([type, data]) => (
                  <CxOption key={type} value={type}>
                    {data.text}
                  </CxOption>
                ))}
              </CxSelect>
            </div>
          </label>
        </div>
        <div className={styles.experiences}>
          {filteredExperiences().sort((a, b) => new Date(b.startDate).getTime() - new Date(a.startDate).getTime()).map((experience) => <ExperienceCard key={experience.id} experience={experience}/>)}
        </div>
      </div>
    );
  }
}
